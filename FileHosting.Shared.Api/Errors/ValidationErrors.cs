using FileHosting.Shared.AppCore.Validation;

namespace FileHosting.Shared.Api.Errors;

internal class ValidationErrors
{
    public static readonly Error InvalidId = new("InvalidId", "Invalid Id");
}