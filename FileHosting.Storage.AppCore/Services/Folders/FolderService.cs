using FileHosting.Shared.AppCore.Exceptions;
using FileHosting.Shared.AppCore.Interfaces;
using FileHosting.Shared.AppCore.UserAccessor;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using FileHosting.Storage.AppCore.Errors;
using FileHosting.Storage.AppCore.Interfaces;
using FileHosting.Storage.AppCore.Specifications.Folders;

namespace FileHosting.Storage.AppCore.Services.Folders;

public class FolderService : IFolderService
{
    private readonly IRepository<Folder> _folderRepository;

    private readonly IStorageService _storageService;

    private readonly IUserAccessor _userAccessor;

    public FolderService(IUserAccessor userAccessor,
        IRepository<Folder> folderRepository,
        IStorageService storageService)
    {
        _userAccessor = userAccessor;
        _folderRepository = folderRepository;
        _storageService = storageService;
    }

    public async Task CreateFolder(string name)
    {
        var result = Folder.Create(name, _userAccessor.UserId);
        result.ThrowIfFailure();

        await _folderRepository.AddAsync(result.Value);
    }

    public async Task<IReadOnlyCollection<FolderVm>> GetAllFolders()
    {
        var folderWithItemsSpecification = new UserFolderWithItemsSpecification(_userAccessor.UserId);
        return await _folderRepository.ListAsync(folderWithItemsSpecification);
    }

    public async Task AddFolderItem(int folderId, string fileName, Stream stream)
    {
        var result = FolderItem.Create(folderId, fileName, fileName);
        result.ThrowIfFailure();

        var folderSpecification = new UserFolderSpecification(folderId, _userAccessor.UserId);
        var folder = await _folderRepository.FirstOrDefaultAsync(folderSpecification);
        if (folder == null)
        {
            throw new DomainException(DomainErrors.Folder.NotFound);
        }

        var folderItem = result.Value;
        folder.AddFolderItem(folderItem);

        var folderItemPath = await _storageService.AddFolderItem(folderItem, stream);
        folderItem.SetPath(folderItemPath).ThrowIfFailure();

        await _folderRepository.SaveChangesAsync();
    }

    public async Task<FolderItemFileDto> GetFolderItemFile(int folderId, int folderItemId)
    {
        var folderItemSpecification = new UserFolderItemSpecification(folderId, folderItemId, _userAccessor.UserId);
        var foldedItem = await _folderRepository.FirstOrDefaultAsync(folderItemSpecification);

        if (foldedItem == null)
        {
            throw new DomainException(DomainErrors.FolderItem.NotFound);
        }

        return new FolderItemFileDto
        {
            Bytes = await _storageService.GetFolderItem(foldedItem),
            FileName = foldedItem.Name
        };
    }
}