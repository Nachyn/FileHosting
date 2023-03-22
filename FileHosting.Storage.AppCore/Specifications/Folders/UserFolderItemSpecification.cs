using Ardalis.Specification;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;

namespace FileHosting.Storage.AppCore.Specifications.Folders;

public class UserFolderItemSpecification : Specification<Folder, FolderItem>
{
    public UserFolderItemSpecification(int folderId, int folderItemId, int userId)
    {
        Query.Select(f => f.Items.FirstOrDefault(fi => fi.Id == folderItemId))
            .Where(f => f.Id == folderId && f.UserId == userId);
    }
}