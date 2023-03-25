using System.Net.Http.Json;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using FileHosting.Storage.AppCore.Services.Folders;
using NUnit.Framework;

namespace FileHosting.Storage.Api.IntegrationTests.FolderEndpoints;

[TestFixture]
public class GetAllFoldersEndpointTests
{
    [OneTimeSetUp]
    public async Task SetUp()
    {
        var folderA1 = Folder.Create("Folder A1", ProgramTest.User.Id).Value;
        var folderA2 = Folder.Create("Folder A2", ProgramTest.User.Id).Value;

        await using var context = ProgramTest.NewContext;
        context.Add(folderA1);
        context.Add(folderA2);
        await context.SaveChangesAsync();

        folderA1.AddFolderItem(FolderItem.Create(folderA1.Id, "Folder Item A1", "test/a1.img").Value);
        folderA2.AddFolderItem(FolderItem.Create(folderA2.Id, "Folder Item A2", "test/a2.img").Value);
        await context.SaveChangesAsync();

        _folders = new List<Folder>
        {
            folderA1,
            folderA2
        };
    }

    private List<Folder> _folders = null!;

    [Test]
    public async Task ReturnsFolders()
    {
        var client = ProgramTest.NewClient;
        var folderA1 = _folders[0];
        var folderA2 = _folders[1];

        var response = await client.GetAsync("api/folders");
        var folders = await response.Content.ReadFromJsonAsync<List<FolderVm>>();

        var apiFolderA1 = folders!.First(f => f.Id == folderA1.Id);
        var apiFolderA2 = folders!.First(f => f.Id == folderA2.Id);

        AssertFolders(folderA1, apiFolderA1);
        AssertFolders(folderA2, apiFolderA2);
    }

    private void AssertFolders(Folder folder, FolderVm folderVm)
    {
        Assert.AreEqual(folder.Id, folderVm.Id);
        Assert.AreEqual(folder.Name, folderVm.Name);
        Assert.AreEqual(folder.UserId, folderVm.UserId);
        Assert.AreEqual(folder.Items.Count, folderVm.Items.Count);
    }
}