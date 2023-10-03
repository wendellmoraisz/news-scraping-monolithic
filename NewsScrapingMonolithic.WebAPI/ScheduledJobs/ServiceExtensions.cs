using NewsScrapingMonolithic.Application.UseCases.SendScrapedNews;
using Quartz;

namespace NewsScrapingMonolithic.WebAPI.ScheduledJobs;

public static class ServiceExtensions
{
    public static void ConfigureScheduledJobs(this IServiceCollection services)
    {
        services.AddScoped<SendScrapedNews>();
        services.AddScoped<NewsScrapingScheduler>();

        services.AddQuartz();

        services.AddQuartzHostedService(opt =>
        {
            opt.WaitForJobsToComplete = true;
        });
    }
}