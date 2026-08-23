using AutoMapper;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.UseCases.CreateNewsPage;

public sealed class CreateNewsPageMapper : Profile
{
    public CreateNewsPageMapper()
    {
        CreateMap<CreateNewsPageRequest, NewsPage>()
            .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url.Trim().ToLowerInvariant()))
            .ForMember(dest => dest.HeaderHost, opt => opt.MapFrom(src => src.HeaderHost.Trim()));

        CreateMap<NewsPage, CreateNewsPageResponse>();
    }
}