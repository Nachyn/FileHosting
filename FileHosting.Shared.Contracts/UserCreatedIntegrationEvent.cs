using EventBus;

namespace FileHosting.Shared.Contracts;

public class UserCreatedIntegrationEvent : IntegrationEvent
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public override string EventKey => UserId.ToString();
}