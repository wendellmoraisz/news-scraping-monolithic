using MediatR;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed record CreateEmailAddressRequest : IRequest<CreateEmailAddressResponse>
{
    public string Address;
    public IEnumerable<string> Hosts;
}