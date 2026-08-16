using AutoMapper;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed class CreateEmailAddressMapper : Profile
{
    public CreateEmailAddressMapper()
    {
        CreateMap<CreateEmailAddressRequest, Email>()
            .ForMember(email => email.Hosts, options => options.Ignore());
        CreateMap<Email, CreateEmailAddressResponse>();
    }
}
