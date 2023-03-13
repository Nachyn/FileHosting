// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace FileHosting.Storage.AppCore.Entities.FolderAggregate;

public class FolderItem : BaseEntity
{
    public string Name { get; private set; } = null!;

    public string Path { get; private set; } = null!;

    public DateTime CreatedDate { get; private set; }

    public int FolderId { get; private set; }
}