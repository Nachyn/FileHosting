namespace FileHosting.Shared.Api.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(IDictionary<string, string[]> errors) : base("ValidationException")
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }
}