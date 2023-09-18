using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Persistence.Context;

namespace NewsScrapingMonolithic.Persistence.Repositories;

public class UnityOfWork : IUnityOfWork
{
    private readonly DataContext _context;

    public UnityOfWork(DataContext context)
    {
        _context = context;
    }

    public Task Save(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}