using Confluent.Kafka;

namespace EventBus.Kafka;

internal class KafkaEventBusSubscription : IEventBusSubscription
{
    private readonly IConsumer<string, string> _consumer;

    private readonly Task _consumerHandler;

    public KafkaEventBusSubscription(IConsumer<string, string> consumer,
        Task consumerHandler)
    {
        _consumer = consumer;
        _consumerHandler = consumerHandler;
    }

    public void Unsubscribe()
    {
        _consumer.Unsubscribe();
        _consumer.Dispose();
    }

    public Task WaitSubscription()
    {
        return _consumerHandler;
    }

    public void Dispose()
    {
        Unsubscribe();
    }
}