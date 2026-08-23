using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Application.Services;
using NewsScrapingMonolithic.Domain.Entities;

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
        IUnityOfWork unityOfWork)
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
            await ProcessNewsPage(newsPage, cancellationToken);
        }
    }

    private async Task ProcessNewsPage(NewsPage newsPage, CancellationToken cancellationToken)
    {
        var newNewsItems = await GetNewNewsItems(newsPage, cancellationToken);
        if (newNewsItems.Count == 0) return;

        var emailsList = await _emailRepository.GetByNewsPageId(newsPage.Id, cancellationToken);
        var newsToCreate = await BuildNewsEntities(newNewsItems, newsPage, cancellationToken);

        await SaveAndNotify(newsToCreate, emailsList, cancellationToken);
    }

    private async Task<List<NewsTitleDto>> GetNewNewsItems(NewsPage newsPage, CancellationToken cancellationToken)
    {
        var extractedNews = await _scrapingService.ExtractNewsTitles(newsPage);
        var extractedTitles = extractedNews.Select(n => n.Title).ToList();

        var existingNews = await _newsRepository.GetByTitles(extractedTitles, cancellationToken);
        var existingTitles = new HashSet<string>(existingNews.Select(n => n.Title));

        return extractedNews.Where(n => !existingTitles.Contains(n.Title)).ToList();
    }

    private async Task<List<News>> BuildNewsEntities(List<NewsTitleDto> newItems, NewsPage newsPage, CancellationToken cancellationToken)
    {
        var newsList = new List<News>();

        foreach (var item in newItems)
        {
            var description = await _scrapingService.GetDescription(newsPage.Url, newsPage.HeaderHost, item.Url);
            newsList.Add(new News
            {
                Title = item.Title,
                Content = description,
                NewsPage = newsPage
            });
        }

        return newsList;
    }

    private async Task SaveAndNotify(List<News> news, List<Email> emails, CancellationToken cancellationToken)
    {
        _newsRepository.CreateRange(news);
        await _unityOfWork.Save(cancellationToken);

        foreach (var n in news)
            await _emailService.Send(emails, n.Title, n.Content);
    }
}
