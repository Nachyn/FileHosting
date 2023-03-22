namespace FileHosting.Storage.Api.FolderEndpoints;

public class DownloadFolderItemRequest
{
    public int FolderId { get; set; }
    
    public int FolderItemId { get; set; }
}