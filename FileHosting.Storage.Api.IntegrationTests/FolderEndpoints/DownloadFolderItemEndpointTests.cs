using System.Net;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using NUnit.Framework;

namespace FileHosting.Storage.Api.IntegrationTests.FolderEndpoints;

[TestFixture]
public class DownloadFolderItemEndpointTests
{
    [OneTimeSetUp]
    public async Task SetUp()
    {
        var folder = Folder.Create("Folder DFI", ProgramTest.User.Id).Value;
        await using var context = ProgramTest.NewContext;
        context.Add(folder);
        await context.SaveChangesAsync();

        var folderItem = FolderItem.Create(folder.Id, "Folder Item A1", "test/a1.img").Value;
        folder.AddFolderItem(folderItem);
        await context.SaveChangesAsync();
        _folderItem = folderItem;
    }

    private FolderItem _folderItem = null!;

    [Test]
    public async Task ReturnsFile()
    {
        var client = ProgramTest.NewClient;

        var response = await client.GetAsync($"api/folders/{_folderItem.FolderId}/folderItems/{_folderItem.Id}");
        var fileBytes = await response.Content.ReadAsByteArrayAsync();

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        Assert.IsTrue(fileBytes.Length > 1);
    }

    [Test]
    public async Task ReturnsNotFound()
    {
        var client = ProgramTest.NewClient;

        var response = await client.GetAsync($"api/folders/8948135/folderItems/{_folderItem.Id}");

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}