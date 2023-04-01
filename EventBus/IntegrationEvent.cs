namespace EventBus;

public abstract class IntegrationEvent
{
    protected IntegrationEvent()
    {
        EventId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public abstract string EventKey { get; }

    public Guid EventId { get; private set; }

    public DateTime CreationDate { get; private init; }
}