using System.Reflection;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace FileHosting.Shared.Api.FluentValidation;

public class Validated<T>
{
    private readonly T _value;

    private readonly ValidationResult _validation;

    private Validated(T value, ValidationResult validation)
    {
        _value = value;
        _validation = validation;
    }

    public T Value => IsValid ? _value : throw new InvalidOperationException("The value of a failure result can not be accessed.");

    public bool IsValid => _validation.IsValid;

    public IDictionary<string, string[]> Errors =>
        _validation.Errors
            .GroupBy(x => x.PropertyName)
            .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray());

    public void Deconstruct(out bool isValid, out T value)
    {
        isValid = IsValid;
        value = Value;
    }

    public static async ValueTask<Validated<T>> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var value = await context.Request.ReadFromJsonAsync<T>();
        var validator = context.RequestServices.GetRequiredService<IValidator<T>>();

        if (value is null)
        {
            throw new ArgumentException(parameter.Name);
        }

        var results = await validator.ValidateAsync(value);

        return new Validated<T>(value, results);
    }
}