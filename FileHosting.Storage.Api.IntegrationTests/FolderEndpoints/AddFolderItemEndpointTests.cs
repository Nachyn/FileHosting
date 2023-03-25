using System.Net;
using System.Text;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FileHosting.Storage.Api.IntegrationTests.FolderEndpoints;

[TestFixture]
public class AddFolderItemEndpointTests
{
    [SetUp]
    public async Task SetUp()
    {
        var folder = Folder.Create("Folder AFI", ProgramTest.User.Id).Value;
        await using var context = ProgramTest.NewContext;
        context.Add(folder);
        await context.SaveChangesAsync();
        _folder = folder;
    }

    private Folder _folder = null!;

    [Test]
    public async Task CreatesFolderItem()
    {
        var client = ProgramTest.NewClient;
        var (fileName, formData) = GetHelloWorldTextFile();

        var response = await client.PostAsync($"api/folders/{_folder.Id}/folderItems", formData);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        await using var context = ProgramTest.NewContext;
        var folderItem = await context.FolderItems.FirstAsync(fi => fi.FolderId == _folder.Id && fi.Name == fileName);
        Assert.IsTrue(folderItem.Path.Contains(fileName));
    }

    [Test]
    public async Task ReturnsNotFound()
    {
        var client = ProgramTest.NewClient;
        var (_, formData) = GetHelloWorldTextFile();

        var response = await client.PostAsync("api/folders/987542/folderItems", formData);

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }

    private (string, MultipartFormDataContent) GetHelloWorldTextFile()
    {
        var bytes = Encoding.UTF8.GetBytes("Hello world");
        var formData = new MultipartFormDataContent();
        var fileName = $"{Guid.NewGuid()}_hello.txt";
        formData.Add(new StreamContent(new MemoryStream(bytes)), "file", fileName);
        return (fileName, formData);
    }
}