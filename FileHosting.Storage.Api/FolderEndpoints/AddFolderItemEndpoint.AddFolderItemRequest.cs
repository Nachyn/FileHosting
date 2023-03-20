namespace FileHosting.Storage.Api.FolderEndpoints;

public class AddFolderItemRequest
{
    public int FolderId { get; set; }

    public IFormFile File { get; set; } = null!;
}