using FileHosting.Shared.AppCore.Validation;

namespace FileHosting.Shared.AppCore.Exceptions;

public static class DomainExceptionExtensions
{
    public static void ThrowIfFailure<T>(this Result<T> result)
    {
        if (result.IsFailure)
        {
            throw new DomainException(result.Error);
        }
    }
}