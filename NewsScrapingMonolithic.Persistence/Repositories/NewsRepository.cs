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

    public Task<News?> GetByTitle(string newsTitle, CancellationToken cancellationToken)
    {
        return Context.Set<News>().FirstOrDefaultAsync(x => x.Title == newsTitle, cancellationToken);
    }

    public async Task<IEnumerable<News>> GetByTitles(IEnumerable<string> newsTitles, CancellationToken cancellationToken)
    {
        var query = Context.Set<News>().Where(x => newsTitles.Contains(x.Title));
        return await query.ToListAsync(cancellationToken);
    }
}