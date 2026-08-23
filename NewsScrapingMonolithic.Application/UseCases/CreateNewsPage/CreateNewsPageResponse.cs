namespace NewsScrapingMonolithic.Application.UseCases.CreateNewsPage;

public sealed record CreateNewsPageResponse
{
    public Guid Id { get; init; }
    public string Url { get; init; } = string.Empty;
    public string HeaderHost { get; init; } = string.Empty;
}