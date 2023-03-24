using FileHosting.Shared.AppCore.UserAccessor;

namespace FileHosting.Storage.Api.IntegrationTests;

public class TestUserAccessor : IUserAccessor
{
    public static int DefaultUserId = 1;

    public int UserId => DefaultUserId;
}