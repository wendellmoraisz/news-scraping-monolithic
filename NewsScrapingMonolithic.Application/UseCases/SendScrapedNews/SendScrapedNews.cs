using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Application.Services;

namespace NewsScrapingMonolithic.Application.UseCases.SendScrapedNews;

public sealed class SendScrapedNews
{
    private readonly IEmailService _emailService;
    private readonly IScrapingService _scrapingService;
    private readonly INewsRepository _newsRepository;
    private readonly IEmailRepository _emailRepository;
    private readonly INewsPageRepository _newsPageRepository;
    private readonly IUnityOfWork _unityOfWork;

    public SendScrapedNews(
        IEmailService emailService,
        IScrapingService scrapingService,
        INewsRepository newsRepository,
        IEmailRepository emailRepository,
        INewsPageRepository newsPageRepository,
        IUnityOfWork unityOfWork
        )
    {
        _emailService = emailService;
        _scrapingService = scrapingService;
        _newsRepository = newsRepository;
        _emailRepository = emailRepository;
        _newsPageRepository = newsPageRepository;
        _unityOfWork = unityOfWork;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        var newsPages = await _newsPageRepository.GetAll(cancellationToken);

        foreach (var newsPage in newsPages)
        {
            var newsList = await _scrapingService.ExtractNews(newsPage);
            var emailsList = await _emailRepository.GetByNewsPageId(newsPage.Id, cancellationToken);

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
}
