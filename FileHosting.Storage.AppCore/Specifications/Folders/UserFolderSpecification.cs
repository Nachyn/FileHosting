using Ardalis.Specification;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;

namespace FileHosting.Storage.AppCore.Specifications.Folders;

public class UserFolderSpecification : Specification<Folder>
{
    public UserFolderSpecification(int folderId, int userId)
    {
        Query.Where(f => f.Id == folderId && f.UserId == userId);
    }
}