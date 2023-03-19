using FileHosting.Shared.AppCore.Entities;
using FileHosting.Shared.AppCore.Interfaces;
using FileHosting.Shared.AppCore.Validation;
using FileHosting.Storage.AppCore.Errors;

namespace FileHosting.Storage.AppCore.Entities;

public class User : BaseEntity, IAggregateRoot
{
    private User()
    {
    }

    public string UserName { get; private set; } = null!;

    public static Result<User> Create(int id, string userName)
    {
        if (id < 1)
        {
            return Result.Failure<User>(DomainErrors.User.InvalidId);
        }

        if (string.IsNullOrWhiteSpace(userName))
        {
            return Result.Failure<User>(DomainErrors.User.EmptyUserName);
        }

        return Result.Success(new User
        {
            UserName = userName,
            Id = id
        });
    }
}