using MediatR;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed record CreateEmailAddressRequest : IRequest<CreateEmailAddressResponse>
{
    public string Address { get; init; } = string.Empty;
    public IReadOnlyCollection<string> Hosts { get; init; } = Array.Empty<string>();
}