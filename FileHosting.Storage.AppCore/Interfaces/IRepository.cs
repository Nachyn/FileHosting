using Ardalis.Specification;

namespace FileHosting.Storage.AppCore.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}