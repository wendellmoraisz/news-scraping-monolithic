using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.Repositories;

public interface IHostRepository : IBaseRepository<Host>
{
    Task<Host?> GetByAddress(string address, CancellationToken cancellationToken);
    Task<List<Host>> GetByAddresses(IReadOnlyCollection<string> addresses, CancellationToken cancellationToken);
}