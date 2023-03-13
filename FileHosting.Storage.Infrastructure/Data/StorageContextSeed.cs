using Microsoft.EntityFrameworkCore;

namespace FileHosting.Storage.Infrastructure.Data;

public static class StorageContextSeed
{
    public static async Task SeedAsync(StorageContext context)
    {
        if (context.Database.IsSqlServer())
        {
            await context.Database.MigrateAsync();
        }
    }
}