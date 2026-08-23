using FluentValidation;
using NewsScrapingMonolithic.Application.Repositories;

namespace NewsScrapingMonolithic.Application.UseCases.CreateNewsPage;

public class CreateNewsPageValidator : AbstractValidator<CreateNewsPageRequest>
{
    private readonly INewsPageRepository _newsPageRepository;

    public CreateNewsPageValidator(INewsPageRepository newsPageRepository)
    {
        _newsPageRepository = newsPageRepository;

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("A URL é obrigatória.")
            .Must(BeValidHttpUrl).WithMessage("A URL deve ser HTTP ou HTTPS.")
            .MustAsync(UrlIsNotRegistered).WithMessage("URL já cadastrada").WithErrorCode("409");

        RuleFor(x => x.HeaderHost)
            .NotEmpty().WithMessage("O HeaderHost é obrigatório.");
    }

    private async Task<bool> UrlIsNotRegistered(string url, CancellationToken cancellationToken)
    {
        var response = await _newsPageRepository.GetByUrl(url, cancellationToken);
        return response is null;
    }

    private static bool BeValidHttpUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }
}