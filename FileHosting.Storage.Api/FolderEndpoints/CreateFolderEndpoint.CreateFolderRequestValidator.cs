using FileHosting.Shared.Api.FluentValidation;
using FileHosting.Shared.AppCore.UserAccessor;
using FileHosting.Storage.AppCore.Entities.FolderAggregate;
using FluentValidation;

namespace FileHosting.Storage.Api.FolderEndpoints;

public class CreateFolderRequestValidator : AbstractValidator<CreateFolderRequest>
{
    public CreateFolderRequestValidator(IUserAccessor userAccessor)
    {
        RuleFor(x => x).MustBeValidResult(x => Folder.Create(x.Name, userAccessor.UserId));
    }
}