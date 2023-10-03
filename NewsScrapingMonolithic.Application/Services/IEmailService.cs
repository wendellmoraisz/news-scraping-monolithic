using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Application.Services;

public interface IEmailService
{
    Task Send(List<Email> emailsAddress, string subject, string content);
}