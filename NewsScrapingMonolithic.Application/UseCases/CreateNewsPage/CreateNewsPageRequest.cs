using MediatR;

namespace NewsScrapingMonolithic.Application.UseCases.CreateNewsPage;

public sealed record CreateNewsPageRequest : IRequest<CreateNewsPageResponse>
{
    public string Url { get; init; } = string.Empty;
    public string HeaderHost { get; init; } = string.Empty;
}