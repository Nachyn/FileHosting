using System.Net;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EventBus.Kafka;

internal class KafkaEventBus : IEventBus, IDisposable
{
    private readonly ILogger<KafkaEventBus> _logger;

    private readonly IProducer<string, string> _producer;

    private readonly KafkaEventBusSettings _settings;

    public KafkaEventBus(KafkaEventBusSettings settings,
        ILogger<KafkaEventBus> logger)
    {
        _settings = settings;
        _logger = logger;

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = _settings.BootstrapServers,
            ClientId = Dns.GetHostName()
            // SaslUsername = _settings.SaslUsername,
            // SaslPassword = _settings.SaslPassword,
            // SecurityProtocol = SecurityProtocol.SaslSsl,
        };

        _producer = new ProducerBuilder<string, string>(producerConfig)
            .Build();
    }

    public void Dispose()
    {
        _producer.Flush();
        _producer.Dispose();
    }

    public async Task Publish(IntegrationEvent integrationEvent)
    {
        var topicName = GetTopicName(integrationEvent);
        await _producer.ProduceAsync(topicName, new Message<string, string>
        {
            Key = integrationEvent.EventKey,
            Timestamp = new Timestamp(integrationEvent.CreationDate),
            Value = JsonConvert.SerializeObject(integrationEvent)
        });
        _logger.LogInformation($"Published {topicName} message");
    }

    public IEventBusSubscription Subscribe<TEvent>(Func<TEvent, Task> eventHandler, CancellationToken cancellationToken)
        where TEvent : IntegrationEvent
    {
        var topicName = GetTopicName<TEvent>();
        var consumerConfig = new ConsumerConfig
        {
            GroupId = topicName,
            BootstrapServers = _settings.BootstrapServers,
            // SaslUsername = _settings.SaslUsername,
            // SaslPassword = _settings.SaslPassword,
            // SecurityProtocol = SecurityProtocol.SaslSsl,
            EnableAutoCommit = false,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            Acks = Acks.All
        };

        var consumer = new ConsumerBuilder<string, string>(consumerConfig)
            .Build();
        consumer.Subscribe(topicName);

        return new KafkaEventBusSubscription(
            consumer,
            Task.Run(() => StartConsumerLoop(consumer, eventHandler, cancellationToken), cancellationToken)
        );
    }

    private async Task StartConsumerLoop<TEvent>(IConsumer<string, string> consumer,
        Func<TEvent, Task> eventHandler,
        CancellationToken cancellationToken) where TEvent : IntegrationEvent
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var consumeResult = consumer.Consume(cancellationToken);
                var message = JsonConvert.DeserializeObject<TEvent>(consumeResult.Message.Value);
                await eventHandler.Invoke(message!);
                consumer.Commit(consumeResult);
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }
    }

    private string GetTopicName<TEvent>() where TEvent : IntegrationEvent
    {
        return typeof(TEvent).Name;
    }

    private string GetTopicName(IntegrationEvent integrationEvent)
    {
        return integrationEvent.GetType().Name;
    }
}