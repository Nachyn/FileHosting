namespace FileHosting.Storage.Infrastructure.Storage;

public class FileStorageSettings
{
    public FileStorageSettings(string rootPath)
    {
        RootPath = rootPath;
    }

    public string RootPath { get; }
}