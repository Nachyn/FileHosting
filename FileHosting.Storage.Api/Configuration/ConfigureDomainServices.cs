using FileHosting.Storage.AppCore.IntegrationEvents;
using FileHosting.Storage.AppCore.Interfaces;
using FileHosting.Storage.AppCore.Services.Folders;

namespace FileHosting.Storage.Api.Configuration;

public static class ConfigureDomainServices
{
    public static IServiceCollection ConfigureStorageAppCore(this IServiceCollection services)
    {
        services.AddScoped<IFolderService, FolderService>();
        services.AddHostedService<UserCreatedIntegrationEventConsumer>();

        return services;
    }
}