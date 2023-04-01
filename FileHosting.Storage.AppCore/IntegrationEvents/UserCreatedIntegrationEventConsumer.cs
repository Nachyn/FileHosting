using EventBus;
using FileHosting.Shared.AppCore.Exceptions;
using FileHosting.Shared.AppCore.Interfaces;
using FileHosting.Shared.Contracts;
using FileHosting.Storage.AppCore.Entities;
using FileHosting.Storage.AppCore.Specifications.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FileHosting.Storage.AppCore.IntegrationEvents;

public class UserCreatedIntegrationEventConsumer : EventConsumer<UserCreatedIntegrationEvent>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserCreatedIntegrationEventConsumer(IEventBus eventBus,
        ILogger<UserCreatedIntegrationEventConsumer> logger,
        IServiceScopeFactory serviceScopeFactory)
        : base(eventBus, logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task Handle(UserCreatedIntegrationEvent integrationEvent)
    {
        var result = User.Create(integrationEvent.UserId, integrationEvent.UserName);
        result.ThrowIfFailure();
        var newUser = result.Value;

        using var scope = _serviceScopeFactory.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IRepository<User>>();

        var user = await userRepository.FirstOrDefaultAsync(new UserSpecification(newUser.Id));
        if (user is not null)
        {
            return;
        }

        await userRepository.AddAsync(newUser);
        await userRepository.SaveChangesAsync();
    }
}