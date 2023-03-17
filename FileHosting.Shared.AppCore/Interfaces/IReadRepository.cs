using Ardalis.Specification;

namespace FileHosting.Shared.AppCore.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}