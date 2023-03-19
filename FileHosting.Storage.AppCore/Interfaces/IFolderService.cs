using FileHosting.Storage.AppCore.Services.Folders;

namespace FileHosting.Storage.AppCore.Interfaces;

public interface IFolderService
{
    Task CreateFolder(string name);

    Task<IReadOnlyCollection<FolderVm>> GetAllFolders();
}