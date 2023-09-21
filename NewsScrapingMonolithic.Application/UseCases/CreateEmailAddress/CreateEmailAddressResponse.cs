namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed record CreateEmailAddressResponse
{
    public Guid Id;
    public string Address;
}