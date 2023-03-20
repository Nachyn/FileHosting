using FileHosting.Storage.AppCore.Entities.FolderAggregate;

namespace FileHosting.Storage.AppCore.Interfaces;

public interface IStorageService
{
    Task<string> AddFolderItem(FolderItem folderItem, Stream stream);

    Task<byte[]> GetFolderItem(FolderItem folderItem);
}