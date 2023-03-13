using Ardalis.Specification.EntityFrameworkCore;
using FileHosting.Storage.AppCore.Interfaces;

namespace FileHosting.Storage.Infrastructure.Data;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T> where T : class, IAggregateRoot
{
    public EfRepository(StorageContext context) : base(context)
    {
    }
}