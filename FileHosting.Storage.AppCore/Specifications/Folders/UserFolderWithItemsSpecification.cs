using Ardalis.Specification;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using FileHosting.Storage.AppCore.Services.Folders;

namespace FileHosting.Storage.AppCore.Specifications.Folders;

public class UserFolderWithItemsSpecification : Specification<Folder, FolderVm>
{
    public UserFolderWithItemsSpecification(int userId)
    {
        Query.Select(f => new FolderVm
        {
            Id = f.Id,
            Name = f.Name,
            UserId = f.UserId,
            Items = f.Items.Select(fi => new FolderItemVm
            {
                Id = fi.Id,
                FolderId = fi.FolderId,
                CreatedDate = fi.CreatedDate
            }).ToList()
        }).Where(f => f.UserId == userId);
    }
}