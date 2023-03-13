using Ardalis.Specification;

namespace FileHosting.Storage.AppCore.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}