namespace EventBus;

public interface IEventBus
{
    Task Publish(IntegrationEvent integrationEvent);

    IEventBusSubscription Subscribe<TEvent>(Func<TEvent, Task> eventHandler, CancellationToken cancellationToken) where TEvent : IntegrationEvent;
}