using Microsoft.EntityFrameworkCore;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;
using NewsScrapingMonolithic.Persistence.Context;

namespace NewsScrapingMonolithic.Persistence.Repositories;

public class NewsRepository : BaseRepository<News>, INewsRepository
{
    public NewsRepository(DataContext context) : base(context)
    {
    }

    public Task<News> GetByTitle(string newsTitle, CancellationToken cancellationToken)
    {
        return Context.Set<News>().FirstAsync(x => x.Title == newsTitle, cancellationToken);
    }
}