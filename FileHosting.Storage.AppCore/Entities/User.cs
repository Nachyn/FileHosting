using FileHosting.Storage.AppCore.Interfaces;

namespace FileHosting.Storage.AppCore.Entities;

public class User : BaseEntity, IAggregateRoot
{
    public string UserName { get; private set; } = null!;
}