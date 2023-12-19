using FluentValidation;
using WebAPI.Entities;

namespace WebAPI.Infrastructure.Validators;

/// <summary>
///     Валидатор адреса
/// </summary>
public class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(x => x.Locality)
            .NotEmpty().WithMessage("Название населённого пункта не должно быть пустым")
            .MaximumLength(50).WithMessage("Слишком много введённых символов");

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Название улицы не должно быть пустым")
            .MaximumLength(50).WithMessage("Слишком много введённых символов");

        RuleFor(x => x.House)
            .NotEmpty().WithMessage("Номер дома не должен быть пустым")
            .MaximumLength(10).WithMessage("Слишком много введённых символов");
    }
}
