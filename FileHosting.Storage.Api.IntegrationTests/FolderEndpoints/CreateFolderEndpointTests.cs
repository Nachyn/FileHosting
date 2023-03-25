using System.Net;
using FileHosting.Shared.Api.IntegrationTests;
using FileHosting.Storage.Api.FolderEndpoints;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace FileHosting.Storage.Api.IntegrationTests.FolderEndpoints;

[TestFixture]
public class CreateFolderEndpointTests
{
    [Test]
    public async Task CreatesFolder()
    {
        var client = ProgramTest.NewClient;
        var newFolderName = Guid.NewGuid().ToString();

        var response = await client.PostAsync("api/folders", new JsonBody(new CreateFolderRequest
        {
            Name = newFolderName
        }));

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

        var context = ProgramTest.NewContext;
        var folder = await context.Folders.FirstAsync(f => f.Name == newFolderName);

        Assert.AreEqual(newFolderName, folder.Name);
    }

    [TestCase(" ")]
    [TestCase(null)]
    public async Task ReturnsInvalidBody(string folderName)
    {
        var client = ProgramTest.NewClient;

        var response = await client.PostAsync("api/folders", new JsonBody(new CreateFolderRequest
        {
            Name = folderName
        }));

        Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
    }
}