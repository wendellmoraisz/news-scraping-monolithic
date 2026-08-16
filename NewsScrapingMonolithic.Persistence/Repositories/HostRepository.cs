using Microsoft.EntityFrameworkCore;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Domain.Entities;
using NewsScrapingMonolithic.Persistence.Context;

namespace NewsScrapingMonolithic.Persistence.Repositories;

public class HostRepository : BaseRepository<Host>, IHostRepository
{
    public HostRepository(DataContext context) : base(context)
    {
    }

    public Task<Host?> GetByAddress(string address, CancellationToken cancellationToken) =>
         Context.Hosts.FirstOrDefaultAsync(x => x.Address == address, cancellationToken: cancellationToken);

    public Task<List<Host>> GetByAddresses(IReadOnlyCollection<string> addresses, CancellationToken cancellationToken) =>
     Context.Hosts.Where(x => addresses.Contains(x.Address)).ToListAsync(cancellationToken: cancellationToken);
}