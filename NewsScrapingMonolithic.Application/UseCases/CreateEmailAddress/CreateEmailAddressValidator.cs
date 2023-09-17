using FluentValidation;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public class CreateEmailAddressValidator : AbstractValidator<CreateEmailAddressRequest>
{
    public CreateEmailAddressValidator()
    {
        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .EmailAddress().WithMessage("Endereço de e-mail iválido");
    }
}