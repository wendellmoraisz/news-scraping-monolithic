using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.Repositories;

public interface INewsRepository : IBaseRepository<News>
{
    Task<News?> GetByTitle(string title, CancellationToken cancellationToken);
    Task<IEnumerable<News>> GetByTitles(IEnumerable<string> titles, CancellationToken cancellationToken);
}