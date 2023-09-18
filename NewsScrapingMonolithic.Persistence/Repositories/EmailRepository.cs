using Microsoft.EntityFrameworkCore;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;
using NewsScrapingMonolithic.Persistence.Context;

namespace NewsScrapingMonolithic.Persistence.Repositories;

public class EmailRepository : BaseRepository<Email>, IEmailRepository
{
    public EmailRepository(DataContext context) : base(context)
    {
    }
    
    public Task<Email> GetByAddress(string emailAddress, CancellationToken cancellationToken)
    {
        return Context.Set<Email>().FirstOrDefaultAsync(x => x.Address == emailAddress, cancellationToken);
    }
}