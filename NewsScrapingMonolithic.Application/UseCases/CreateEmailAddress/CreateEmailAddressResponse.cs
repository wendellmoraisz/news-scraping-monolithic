namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed record CreateEmailAddressResponse
{
    public Guid Id { get; init; }
    public string Address { get; init; } = string.Empty;
}