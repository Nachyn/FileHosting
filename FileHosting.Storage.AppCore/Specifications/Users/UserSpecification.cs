using Ardalis.Specification;
using FileHosting.Storage.AppCore.Entities;

namespace FileHosting.Storage.AppCore.Specifications.Users;

public class UserSpecification : Specification<User>
{
    public UserSpecification(int userId)
    {
        Query.Where(u => u.Id == userId);
    }
}