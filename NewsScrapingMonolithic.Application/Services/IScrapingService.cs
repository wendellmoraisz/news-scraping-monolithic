using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.Services;

public interface IScrapingService
{
    Task<IEnumerable<NewsTitleDto>> ExtractNewsTitles(NewsPage newsPage);
    Task<string> GetDescription(string baseUrl, string host, string descUrl);
}

public record NewsTitleDto(string Title, string Url);