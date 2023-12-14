using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Application.Services;
using NewsScrapingMonolithic.Persistence.Context;
using NewsScrapingMonolithic.Persistence.Repositories;
using NewsScrapingMonolithic.Persistence.Services;

namespace NewsScrapingMonolithic.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("NewsScrapingDbConfig");
        services.AddDbContext<DataContext>(opts =>
        opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IEmailRepository, EmailRepository>();
        services.AddScoped<INewsRepository, NewsRepository>();
        services.AddScoped<IScrapingService, ScrapingService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}