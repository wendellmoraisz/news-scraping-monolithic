using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Application.Services;

namespace NewsScrapingMonolithic.Application.UseCases.SendScrapedNews;

public sealed class SendScrapedNews
{
    private readonly IEmailService _emailService;
    private readonly IScrapingService _scrapingService;
    private readonly INewsRepository _newsRepository;
    private readonly IEmailRepository _emailRepository;
    private readonly IUnityOfWork _unityOfWork;

    public SendScrapedNews(
        IEmailService emailService,
        IScrapingService scrapingService,
        INewsRepository newsRepository,
        IEmailRepository emailRepository,
        IUnityOfWork unityOfWork
        )
    {
        _emailService = emailService;
        _scrapingService = scrapingService;
        _newsRepository = newsRepository;
        _emailRepository = emailRepository;
        _unityOfWork = unityOfWork;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        var newsList = await _scrapingService.ExtractNews();
        var emailsList = await _emailRepository.GetAll(cancellationToken);

        foreach (var news in newsList)
        {
            var newsIsAlreadySent = await _newsRepository.GetByTitle(news.Title, cancellationToken) != null;
            if (newsIsAlreadySent) continue;
            _newsRepository.Create(news);
            await _unityOfWork.Save(cancellationToken);
            await _emailService.Send(emailsList, news.Title, news.Content);
        }
    }
}