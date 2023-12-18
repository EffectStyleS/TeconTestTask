using FluentValidation;

namespace WebAPI.Infrastructure.Extensions;

public static class CustomValidations
{
    /// <summary>
    ///     
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ruleBuilder"></param>
    /// <param name="minPasswordLength"></param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> PasswordCustom<T>(this IRuleBuilder<T, string> ruleBuilder, int minPasswordLength = 8)
    {
        return ruleBuilder.MinimumLength(minPasswordLength).WithMessage("Минимальная длина пароля - " + minPasswordLength + " символов")
                          .Matches("[a-z]+").WithMessage("Пароль должен содержать минимум 1 строчную букву")
                          .Matches("[A-Z]+").WithMessage("Пароль должен содержать минимум 1 заглавную букву")
                          .Matches("[0-9]+").WithMessage("Пароль должен содержать минимум 1 цифру");
    }
}
