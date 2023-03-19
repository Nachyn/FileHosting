using FileHosting.Shared.AppCore.Validation;
using FluentValidation;

namespace FileHosting.Shared.Api.FluentValidation;

public static class FluentValidationExtensions
{
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
}