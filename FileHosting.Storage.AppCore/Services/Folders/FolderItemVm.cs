namespace FileHosting.Storage.AppCore.Services.Folders;

public class FolderItemVm
{
    public int Id { get; set; }

    public string Name { get; private set; } = null!;

    public string Path { get; private set; } = null!;

    public DateTime CreatedDate { get; set; }

    public int FolderId { get; set; }
}