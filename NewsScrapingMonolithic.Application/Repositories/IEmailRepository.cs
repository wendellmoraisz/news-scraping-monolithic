using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.Repositories;

public interface IEmailRepository : IBaseRepository<Email>
{
    Task<Email?> GetByAddress(string emailAddress, CancellationToken cancellationToken);
    Task<List<Email>> GetByHost(string host, CancellationToken cancellationToken);
}