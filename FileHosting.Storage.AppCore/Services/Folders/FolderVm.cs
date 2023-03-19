namespace FileHosting.Storage.AppCore.Services.Folders;

public class FolderVm
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int UserId { get; set; }

    public IReadOnlyCollection<FolderItemVm> Items { get; set; } = new List<FolderItemVm>();
}