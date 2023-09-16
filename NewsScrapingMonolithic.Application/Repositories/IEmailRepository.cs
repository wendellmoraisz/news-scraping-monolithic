using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.Repositories;

public interface IEmailRepository : IBaseRepository<Email>
{
    Task<Email> GetByAddress(string emailAddress);
}