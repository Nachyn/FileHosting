using Ardalis.Specification;

namespace FileHosting.Shared.AppCore.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}