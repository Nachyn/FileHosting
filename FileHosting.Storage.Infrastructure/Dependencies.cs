using FileHosting.Storage.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FileHosting.Storage.Infrastructure;

public static class Dependencies
{
    public static void ConfigureStorageInfrastructure(this IServiceCollection services, IConfiguration configuration)
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
    }
}