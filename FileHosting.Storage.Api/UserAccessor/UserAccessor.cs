using FileHosting.Shared.AppCore.UserAccessor;

namespace FileHosting.Storage.Api.UserAccessor;

public class UserAccessor : IUserAccessor
{
    private readonly HttpContext _httpContext;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContext = httpContextAccessor.HttpContext!;
    }

    public int UserId => int.Parse(_httpContext.User.Identity!.Name!);
}