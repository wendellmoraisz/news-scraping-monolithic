using MediatR;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed record CreateEmailAddressRequest(string Address) : IRequest<CreateEmailAddressResponse>;