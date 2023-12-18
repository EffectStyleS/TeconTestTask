using FluentValidation;
using WebAPI.DTOs;

namespace WebAPI.Infrastructure.Validators;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator() 
    {
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
