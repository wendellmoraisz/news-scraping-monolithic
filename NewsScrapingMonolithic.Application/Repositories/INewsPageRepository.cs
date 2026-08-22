using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.Repositories;

public interface INewsPageRepository : IBaseRepository<NewsPage>
{
    Task<NewsPage?> GetByUrl(string url, CancellationToken cancellationToken);
    Task<List<NewsPage>> GetByUrls(IReadOnlyCollection<string> urls, CancellationToken cancellationToken);
}