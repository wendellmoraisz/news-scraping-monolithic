using AutoMapper;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.UseCases.CreateEmailAddress;

public sealed class CreateEmailAddressMapper : Profile
{
    public CreateEmailAddressMapper()
    {
        CreateMap<CreateEmailAddressRequest, Email>();
        CreateMap<Email, CreateEmailAddressResponse>();
    }
}