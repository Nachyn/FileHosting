using FileHosting.Shared.Api.Exceptions;

namespace FileHosting.Shared.Api.FluentValidation;

public static class ValidatedExtensions
{
    public static void ThrowIfFailure<T>(this Validated<T> result)
    {
        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors);
        }
    }
}