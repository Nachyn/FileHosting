using FileHosting.Shared.AppCore.Interfaces;
using FileHosting.Shared.Infrastructure.Data;

namespace FileHosting.Storage.Infrastructure.Data;

public class StorageRepository<T> : EfRepository<T, StorageContext> where T : class, IAggregateRoot
{
    public StorageRepository(StorageContext context) : base(context)
    {
    }
}