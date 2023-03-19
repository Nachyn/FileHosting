using FileHosting.Storage.AppCore.Interfaces;
using FileHosting.Storage.AppCore.Services.Folders;

namespace FileHosting.Storage.Api.Configuration;

public static class ConfigureDomainServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddScoped<IFolderService, FolderService>();

        return services;
    }
}