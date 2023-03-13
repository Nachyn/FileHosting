using System.Reflection;
using FileHosting.Storage.AppCore.Entities;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using Microsoft.EntityFrameworkCore;

namespace FileHosting.Storage.Infrastructure.Data;

public class StorageContext : DbContext
{
#pragma warning disable CS8618
    public StorageContext(DbContextOptions<StorageContext> options) : base(options)
#pragma warning restore CS8618
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Folder> Folders { get; set; }

    public DbSet<FolderItem> FolderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}