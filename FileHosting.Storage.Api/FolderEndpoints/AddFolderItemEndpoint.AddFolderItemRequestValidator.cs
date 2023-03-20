using FileHosting.Shared.Api.FluentValidation;
using FluentValidation;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class AddFolderItemRequestValidator : AbstractValidator<AddFolderItemRequest>
{
    public AddFolderItemRequestValidator()
    {
        RuleFor(x => x.FolderId).MustBeValidId();
        RuleFor(x => x.File).NotEmpty();
    }
}