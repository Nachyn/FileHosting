using Ardalis.Specification.EntityFrameworkCore;
using FileHosting.Shared.AppCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileHosting.Shared.Infrastructure.Data;

public class EfRepository<T, TContext> : RepositoryBase<T>, IRepository<T>, IReadRepository<T>
    where T : class, IAggregateRoot
    where TContext : DbContext
{
    public EfRepository(TContext context) : base(context)
    {
    }
}