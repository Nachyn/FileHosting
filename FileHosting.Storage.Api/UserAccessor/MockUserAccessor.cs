using FileHosting.Shared.AppCore.UserAccessor;

namespace FileHosting.Storage.Api.UserAccessor;

public class MockUserAccessor : IUserAccessor
{
    public int UserId => 1;
}