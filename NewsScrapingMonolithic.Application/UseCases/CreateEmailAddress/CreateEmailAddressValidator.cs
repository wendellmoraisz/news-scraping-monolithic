using FluentValidation;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;

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

        RuleFor(x => x.NewsPages)
            .NotEmpty().WithMessage("Informe ao menos uma página de notícias.")
            .Must(HaveUniqueUrls).WithMessage("As URLs das páginas de notícias devem ser únicas.");

        RuleForEach(x => x.NewsPages)
            .NotNull()
            .ChildRules(newsPage =>
            {
                newsPage.RuleFor(x => x.Url)
                    .NotEmpty().WithMessage("A URL da página de notícias é obrigatória.")
                    .Must(BeValidHttpUrl).WithMessage("A URL da página de notícias deve ser HTTP ou HTTPS.");

                newsPage.RuleFor(x => x.HeaderHost)
                    .NotEmpty().WithMessage("O HeaderHost da página de notícias é obrigatório.");
            });
    }

    private async Task<bool> EmailIsNotRegistered(string emailAddress, CancellationToken cancellationToken)
    {
        var response = await _emailRepository.GetByAddress(emailAddress, cancellationToken);
        return response is null;
    }

    private static bool HaveUniqueUrls(IReadOnlyCollection<NewsPage> newsPages)
    {
        if (newsPages.Any(newsPage => newsPage is null || string.IsNullOrWhiteSpace(newsPage.Url)))
        {
            return true;
        }

        return newsPages
            .Select(newsPage => newsPage.Url.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Count() == newsPages.Count;
    }

    private static bool BeValidHttpUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}
