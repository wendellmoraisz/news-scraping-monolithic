using NewsScrapingMonolithic.Application;
using NewsScrapingMonolithic.Persistence;
using NewsScrapingMonolithic.Persistence.Context;
using NewsScrapingMonolithic.Persistence.Credentials;
using NewsScrapingMonolithic.WebAPI.Extensions;
using NewsScrapingMonolithic.WebAPI.ScheduledJobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();

builder.Services.ConfigureScheduledJobs();

builder.Services.Configure<EmailServiceCredentials>(builder.Configuration.GetSection("EmailServiceCredentials"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var serviceScope = app.Services.CreateScope();

var schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

var job = JobBuilder.Create<NewsScrapingScheduler>()
    .WithIdentity("send-scraped-news", "news")
    .Build();

var trigger = TriggerBuilder.Create()
    .WithIdentity("send-scraped-news-minutely", "news")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(120)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);

var dataContext = serviceScope.ServiceProvider.GetService<DataContext>();
dataContext?.Database.EnsureCreated();

if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseErrorHandler();
app.UseCors();
app.MapControllers();
app.Run();