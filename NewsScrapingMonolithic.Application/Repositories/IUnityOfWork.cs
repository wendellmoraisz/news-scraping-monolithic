namespace NewsScrapingMonolithic.Application.Repositories;

public interface IUnityOfWork
{
    Task Save(CancellationToken cancellationToken);
}