using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using FileHosting.Storage.AppCore.Interfaces;

namespace FileHosting.Storage.Infrastructure.Storage;

public class FileStorageService : IStorageService
{
    private readonly FileStorageSettings _settings;

    public FileStorageService(FileStorageSettings settings)
    {
        _settings = settings;
    }

    public async Task<string> AddFolderItem(FolderItem folderItem, Stream stream)
    {
        var folderItemPath = Path.Combine(folderItem.FolderId.ToString(),
            $"{DateTime.Now:yyyyMMddHHmmss}_{Guid.NewGuid():N}_{folderItem.Name}");
        var fullPath = Path.Combine(_settings.RootPath, folderItemPath);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);

        await using var fileStream = new FileStream(fullPath, FileMode.CreateNew);
        await using (stream)
        {
            await stream.CopyToAsync(fileStream);
        }

        return folderItemPath;
    }

    public async Task<byte[]> GetFolderItem(FolderItem folderItem)
    {
        var fullPath = Path.Combine(_settings.RootPath, folderItem.Path);
        return await File.ReadAllBytesAsync(fullPath);
    }
}