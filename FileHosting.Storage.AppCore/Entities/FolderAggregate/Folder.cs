using FileHosting.Shared.AppCore.Entities;
using FileHosting.Shared.AppCore.Exceptions;
using FileHosting.Shared.AppCore.Interfaces;
using FileHosting.Shared.AppCore.Validation;
using FileHosting.Storage.AppCore.Errors;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace FileHosting.Storage.AppCore.Entities.FolderAggregate;

public class Folder : BaseEntity, IAggregateRoot
{
    private readonly List<FolderItem> _items = new();

    private Folder()
    {
    }

    public string Name { get; private set; } = null!;

    public int UserId { get; private set; }
    public User? User { get; private set; }

    public IReadOnlyCollection<FolderItem> Items => _items.AsReadOnly();

    public void AddFolderItem(FolderItem folderItem)
    {
        if (folderItem == null)
        {
            throw new ArgumentException(nameof(folderItem));
        }

        _items.Add(folderItem);
    }

    public static Result<Folder> Create(string name, int userId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Failure<Folder>(DomainErrors.Folder.EmptyName);
        }

        if (userId < 1)
        {
            return Result.Failure<Folder>(DomainErrors.Folder.InvalidUserId);
        }

        return Result.Success(new Folder
        {
            Name = name,
            UserId = userId,
            Id = 0
        });
    }
}