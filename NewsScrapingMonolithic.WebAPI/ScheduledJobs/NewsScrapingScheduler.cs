using NewsScrapingMonolithic.Application.UseCases.SendScrapedNews;
using Quartz;

namespace NewsScrapingMonolithic.WebAPI.ScheduledJobs;

public class NewsScrapingScheduler : IJob
{
    private readonly SendScrapedNews _sendScrapedNews;

    public NewsScrapingScheduler(SendScrapedNews sendScrapedNews)
    {
        _sendScrapedNews = sendScrapedNews;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _sendScrapedNews.Execute(CancellationToken.None);
    }
    
}