using FileHosting.Shared.AppCore.Validation;

namespace FileHosting.Shared.AppCore.Exceptions;

public class DomainException : Exception
{
    public DomainException(Error error) : base($"{error.Code}: {error.Message}")
    {
        Error = error;
    }

    public Error Error { get; }
}