using FileHosting.Shared.AppCore.Interfaces;
using FileHosting.Shared.Infrastructure.Logging;
using FileHosting.Storage.AppCore.Interfaces;
using FileHosting.Storage.Infrastructure.Data;
using FileHosting.Storage.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileHosting.Storage.Infrastructure;

public static class Dependencies
{
    public static void ConfigureStorageInfrastructure(this IServiceCollection services,
        IConfiguration configuration,
        string rootFilePath)
    {
        AddDatabase(services, configuration);
        AddStorage(services, rootFilePath);
        services.AddSingleton(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var useInMemoryDatabaseConfig = configuration["UseInMemoryDatabase"];
        var useInMemoryDatabase = useInMemoryDatabaseConfig != null && bool.Parse(useInMemoryDatabaseConfig);

        if (useInMemoryDatabase)
        {
            services.AddDbContext<StorageContext>(c => c.UseInMemoryDatabase(nameof(StorageContext)));
        }
        else
        {
            services.AddDbContext<StorageContext>(c => c.UseSqlServer(configuration.GetConnectionString("StorageContext")));
        }

        services.AddScoped(typeof(IRepository<>), typeof(StorageRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(StorageRepository<>));
    }

    private static void AddStorage(IServiceCollection services, string rootFilePath)
    {
        services.AddSingleton(new FileStorageSettings(rootFilePath));
        services.AddSingleton<IStorageService, FileStorageService>();
    }
}