using FileHosting.Storage.AppCore.Interfaces;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace FileHosting.Storage.AppCore.Entities.FolderAggregate;

public class Folder : BaseEntity, IAggregateRoot
{
    private readonly List<FolderItem> _items = new();

    public string Name { get; private set; } = null!;

    public int UserId { get; private set; }
    public User? User { get; private set; }

    public IReadOnlyCollection<FolderItem> Items => _items.AsReadOnly();
}