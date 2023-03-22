using FileHosting.Storage.AppCore.Services.Folders;

namespace FileHosting.Storage.AppCore.Interfaces;

public interface IFolderService
{
    Task CreateFolder(string name);

    Task<IReadOnlyCollection<FolderVm>> GetAllFolders();

    Task AddFolderItem(int folderId, string fileName, Stream stream);

    Task<FolderItemFileDto> GetFolderItemFile(int folderId, int folderItemId);
}