using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsScrapingMonolithic.Application.Repositories;
using NewsScrapingMonolithic.Persistence.Context;
using NewsScrapingMonolithic.Persistence.Repositories;

namespace NewsScrapingMonolithic.Persistence;

public static class ServiceExtensions
{
    public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("mysql");
        services.AddDbContext<DataContext>(opts =>
        opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddScoped<IUnityOfWork, UnityOfWork>();
        services.AddScoped<IEmailRepository, EmailRepository>();
    }
}