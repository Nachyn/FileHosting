namespace EventBus;

public interface IEventBusSubscription : IDisposable
{
    void Unsubscribe();
    Task WaitSubscription();
}