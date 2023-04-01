using MediatR;

namespace FileHosting.Identity.Api.Events;

public class UserCreatedEvent : INotification
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string UserName { get; set; } = null!;
}