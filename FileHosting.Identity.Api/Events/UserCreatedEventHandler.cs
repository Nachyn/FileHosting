using EventBus;
using FileHosting.Shared.Contracts;
using MediatR;

namespace FileHosting.Identity.Api.Events;

public class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly IEventBus _eventBus;

    public UserCreatedEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(UserCreatedEvent userCreatedEvent, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(userCreatedEvent.Email)
            || string.IsNullOrWhiteSpace(userCreatedEvent.Email)
            || userCreatedEvent.UserId < 1)
        {
            throw new ArgumentException(nameof(userCreatedEvent));
        }

        await _eventBus.Publish(new UserCreatedIntegrationEvent
        {
            Email = userCreatedEvent.Email,
            UserId = userCreatedEvent.UserId,
            UserName = userCreatedEvent.UserName
        });
    }
}