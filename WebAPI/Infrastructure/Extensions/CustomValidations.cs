using FluentValidation;

namespace WebAPI.Infrastructure.Extensions;

/// <summary>
///      Кастомные валидации, расширяют IRuleBuilder
/// </summary>
public static class CustomValidations
{
    /// <summary>
    ///     Валидация пароля
    /// </summary>
    /// <typeparam name="T">Тип проверяемого объекта</typeparam>
    /// <param name="ruleBuilder">Валидатор</param>
    /// <param name="minPasswordLength">
    ///     Минимальная длина пароля, по умолчанию 8 символов
    /// </param>
    /// <returns></returns>
    public static IRuleBuilderOptions<T, string> PasswordCustom<T>(this IRuleBuilder<T, string> ruleBuilder, int minPasswordLength = 8)
    {
        return ruleBuilder.MinimumLength(minPasswordLength).WithMessage("Минимальная длина пароля - " + minPasswordLength + " символов")
                          .Matches("[a-z]+").WithMessage("Пароль должен содержать минимум 1 строчную букву")
                          .Matches("[A-Z]+").WithMessage("Пароль должен содержать минимум 1 заглавную букву")
                          .Matches("[0-9]+").WithMessage("Пароль должен содержать минимум 1 цифру");
    }
}
