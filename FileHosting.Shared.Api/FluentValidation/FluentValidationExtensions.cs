using FileHosting.Shared.Api.Errors;
using FileHosting.Shared.AppCore.Validation;
using FluentValidation;
using ValidationException = FileHosting.Shared.Api.Exceptions.ValidationException;

namespace FileHosting.Shared.Api.FluentValidation;

public static class FluentValidationExtensions
{
    public static void ValidateAndThrowIfInvalid<T>(this AbstractValidator<T> validator, T value)
    {
        var validationResult = validator.Validate(value);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(x => x.Key, x => x.Select(e => e.ErrorMessage).ToArray()));
        }
    }

    public static IRuleBuilderOptions<T, T> MustBeValidResult<T, TValueObject>(
        this IRuleBuilder<T, T> ruleBuilder,
        Func<T, Result<TValueObject>> factoryMethod)
    {
        return (IRuleBuilderOptions<T, T>) ruleBuilder.Custom((value, context) =>
        {
            var result = factoryMethod(value);

            if (result.IsFailure)
            {
                context.AddFailure(result.Error.Code, result.Error.Message);
            }
        });
    }

    public static IRuleBuilderOptions<T, int> MustBeValidId<T>(
        this IRuleBuilder<T, int> ruleBuilder)
    {
        return (IRuleBuilderOptions<T, int>) ruleBuilder.Custom((value, context) =>
        {
            if (value < 1)
            {
                context.AddFailure(ValidationErrors.InvalidId.Code, ValidationErrors.InvalidId.Message);
            }
        });
    }
}