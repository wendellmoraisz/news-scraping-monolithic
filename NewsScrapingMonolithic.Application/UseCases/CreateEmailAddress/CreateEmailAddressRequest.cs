using MediatR;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed record CreateEmailAddressRequest : IRequest<CreateEmailAddressResponse>
{
    public string Address { get; init; } = string.Empty;
    public IReadOnlyCollection<NewsPage> NewsPages { get; init; } = Array.Empty<NewsPage>();
}