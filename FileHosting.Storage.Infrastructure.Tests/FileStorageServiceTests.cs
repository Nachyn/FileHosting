using System.Text;
using FileHosting.Shared.AppCore.Exceptions;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using FileHosting.Storage.AppCore.Interfaces;
using FileHosting.Storage.Infrastructure.Storage;
using NUnit.Framework;

namespace FileHosting.Storage.Infrastructure.Tests;

[TestFixture]
public class FileStorageServiceTests
{
    [OneTimeSetUp]
    public void SetUp()
    {
        _storageService =
            new FileStorageService(new FileStorageSettings(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "files")));
        _folderItem = FolderItem.Create(1, FileName, FileName).Value;
    }

    private IStorageService _storageService = null!;

    private FolderItem _folderItem = null!;

    private const string FileName = "hello.txt";

    [Test]
    [Order(1)]
    public async Task AddFolderItem()
    {
        var bytes = Encoding.UTF8.GetBytes("Hello world");
        var stream = new MemoryStream(bytes);

        var filePath = await _storageService.AddFolderItem(_folderItem, stream);
        _folderItem.SetPath(filePath).ThrowIfFailure();

        Assert.AreEqual(FileName, _folderItem.Name);
        Assert.IsTrue(_folderItem.Path.Length > _folderItem.Name.Length);
        Assert.IsTrue(_folderItem.Path.Contains(_folderItem.Name));
    }

    [Test]
    [Order(2)]
    public async Task GetFolderItem()
    {
        var bytes = await _storageService.GetFolderItem(_folderItem);
        Assert.IsTrue(bytes.Length > 0);
    }
}