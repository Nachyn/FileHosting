using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventBus;

public abstract class EventConsumer<TEvent> : BackgroundService where TEvent : IntegrationEvent
{
    private readonly IEventBus _eventBus;

    private readonly ILogger<EventConsumer<TEvent>> _logger;

    private IEventBusSubscription? _eventBusSubscription;

    protected EventConsumer(IEventBus eventBus,
        ILogger<EventConsumer<TEvent>> logger)
    {
        _eventBus = eventBus;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation($"Start consume {typeof(TEvent).Name}");
        _eventBusSubscription = _eventBus.Subscribe<TEvent>(Handle, stoppingToken);
        await _eventBusSubscription.WaitSubscription();
    }

    protected abstract Task Handle(TEvent integrationEvent);

    public override void Dispose()
    {
        _eventBusSubscription?.Unsubscribe();
        base.Dispose();
    }
}