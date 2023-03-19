using FileHosting.Shared.AppCore.Exceptions;
using FileHosting.Shared.AppCore.Interfaces;
using FileHosting.Shared.AppCore.UserAccessor;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using FileHosting.Storage.AppCore.Interfaces;

namespace FileHosting.Storage.AppCore.Services;

public class FolderService : IFolderService
{
    private readonly IRepository<Folder> _folderRepository;

    private readonly IUserAccessor _userAccessor;

    public FolderService(IUserAccessor userAccessor,
        IRepository<Folder> folderRepository)
    {
        _userAccessor = userAccessor;
        _folderRepository = folderRepository;
    }

    public async Task CreateFolder(string name)
    {
        var result = Folder.Create(name, _userAccessor.UserId);
        result.ThrowIfFailure();

        await _folderRepository.AddAsync(result.Value);
    }
}