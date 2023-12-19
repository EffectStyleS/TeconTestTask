using FluentValidation;
using WebAPI.DTOs;
using WebAPI.Infrastructure.Extensions;

namespace WebAPI.Infrastructure.Validators;

/// <summary>
///     Валидатор DTO регистрации
/// </summary>
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email).EmailAddress().WithMessage("Неверный формат почты");

        RuleFor(x => x.Password).PasswordCustom();

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Имя не должно быть пустым")
            .MaximumLength(50).WithMessage("Слишком много введённых символов");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Фамилия не должна быть пустой")
            .MaximumLength(50).WithMessage("Слишком много введённых символов");

        RuleFor(x => x.Patronymic)
            .NotEmpty().WithMessage("Отчество не должно быть пустым")
            .MaximumLength(50).WithMessage("Слишком много введённых символов");

        RuleFor(x => x.Address).SetValidator(new AddressValidator());
    }
}
