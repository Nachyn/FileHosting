namespace FileHosting.Storage.AppCore.Services.Folders;

public class FolderItemFileDto
{
    public string FileName { get; set; } = null!;

    public byte[] Bytes { get; set; } = null!;
}