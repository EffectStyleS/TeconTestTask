using FluentValidation;
using WebAPI.DTOs;
using WebAPI.Infrastructure.Extensions;

namespace WebAPI.Infrastructure.Validators;

public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Введите текущий пароль");

        RuleFor(x => x.NewPassword)
            .NotEqual(x => x.CurrentPassword).WithMessage("Новый пароль не должен совпадать со старым")
            .PasswordCustom();
    }
}
