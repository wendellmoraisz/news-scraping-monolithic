using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Application.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<T> Get(Guid id);
    Task<List<T>> GetAll();
}