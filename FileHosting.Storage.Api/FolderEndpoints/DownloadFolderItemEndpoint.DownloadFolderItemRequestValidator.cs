using FileHosting.Shared.Api.FluentValidation;
using FluentValidation;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class DownloadFolderItemRequestValidator : AbstractValidator<DownloadFolderItemRequest>
{
    public DownloadFolderItemRequestValidator()
    {
        RuleFor(x => x.FolderId).MustBeValidId();
        RuleFor(x => x.FolderItemId).MustBeValidId();
    }
}