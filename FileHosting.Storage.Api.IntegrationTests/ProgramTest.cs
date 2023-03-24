using FileHosting.Storage.AppCore.Entities;
using FileHosting.Storage.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace FileHosting.Storage.Api.IntegrationTests;

[SetUpFixture]
public class ProgramTest
{
    private static WebAppFactory _app = null!;

    public static User User = null!;

    public static HttpClient NewClient => _app.CreateClient();

    public static StorageContext NewContext => _app.Services.GetRequiredService<IServiceScopeFactory>()
        .CreateScope().ServiceProvider.GetRequiredService<StorageContext>();

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _app = new WebAppFactory();
        SeedDatabase(NewContext);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        _app.Dispose();
    }

    private void SeedDatabase(StorageContext context)
    {
        User = User.Create(TestUserAccessor.DefaultUserId, "Alex").Value;
        context.Users.Add(User);
        context.SaveChanges();
    }
}