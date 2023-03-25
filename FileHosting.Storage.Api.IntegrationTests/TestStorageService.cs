using System.Text;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using FileHosting.Storage.AppCore.Interfaces;

namespace FileHosting.Storage.Api.IntegrationTests;

public class TestStorageService : IStorageService
{
    public Task<string> AddFolderItem(FolderItem? folderItem, Stream? stream)
    {
        if (folderItem is null || stream is null)
        {
            throw new ArgumentException(nameof(AddFolderItem));
        }

        return Task.FromResult(folderItem.Path);
    }

    public Task<byte[]> GetFolderItem(FolderItem? folderItem)
    {
        if (folderItem is null || string.IsNullOrWhiteSpace(folderItem.Path))
        {
            throw new ArgumentException(nameof(AddFolderItem));
        }

        return Task.FromResult(Encoding.UTF8.GetBytes("Hello world"));
    }
}