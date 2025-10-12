using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.Services;

public interface IScrapingService
{
    Task<IEnumerable<News>> ExtractNews(string baseUrl, string host);
}