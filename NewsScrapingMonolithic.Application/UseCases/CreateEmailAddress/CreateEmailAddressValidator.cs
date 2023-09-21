using FluentValidation;
using NewsScrapingMonolithic.Application.Repositories;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public class CreateEmailAddressValidator : AbstractValidator<CreateEmailAddressRequest>
{
    private readonly IEmailRepository _emailRepository;
    
    public CreateEmailAddressValidator(IEmailRepository emailRepository)
    {
        _emailRepository = emailRepository;

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .EmailAddress().WithMessage("Endereço de e-mail inválido")
            .MustAsync(EmailIsNotRegistered).WithMessage("E-mail já cadastrado").WithErrorCode("409");
    }

    private async Task<bool> EmailIsNotRegistered(string emailAddress, CancellationToken cancellationToken)
    {
        var response = await _emailRepository.GetByAddress(emailAddress, cancellationToken);
        return response is null;
    }
}