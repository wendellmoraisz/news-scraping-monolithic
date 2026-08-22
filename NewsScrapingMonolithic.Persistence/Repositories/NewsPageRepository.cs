using Microsoft.EntityFrameworkCore;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;
using NewsScrapingMonolithic.Persistence.Context;

namespace NewsScrapingMonolithic.Persistence.Repositories;

public class NewsPageRepository : BaseRepository<NewsPage>, INewsPageRepository
{
     public NewsPageRepository(DataContext context) : base(context)
     {
     }

     public Task<NewsPage?> GetByUrl(string url, CancellationToken cancellationToken) =>
          Context.NewsPages.FirstOrDefaultAsync(x => x.Url == url, cancellationToken: cancellationToken);

     public Task<List<NewsPage>> GetByUrls(IReadOnlyCollection<string> urls, CancellationToken cancellationToken) =>
      Context.NewsPages.Where(x => urls.Contains(x.Url)).ToListAsync(cancellationToken: cancellationToken);
}