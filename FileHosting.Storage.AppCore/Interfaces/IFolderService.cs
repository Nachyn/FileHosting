namespace FileHosting.Storage.AppCore.Interfaces;

public interface IFolderService
{
    Task CreateFolder(string name);
}