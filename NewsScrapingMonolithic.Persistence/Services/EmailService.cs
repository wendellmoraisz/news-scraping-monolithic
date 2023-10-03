using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using NewsScrapingMonolithic.Application.Services;
using NewsScrapingMonolithic.Domain.Entities;
using NewsScrapingMonolithic.Persistence.Credentials;

namespace NewsScrapingMonolithic.Persistence.Services;

public class EmailService : IEmailService
{
    private readonly EmailServiceCredentials _emailServiceCredentials;
    private readonly SmtpClient _smtpClient;

    public EmailService(IOptions<EmailServiceCredentials> options)
    {
        _emailServiceCredentials = options.Value;
        _smtpClient = new SmtpClient(_emailServiceCredentials.SmtpHost)
        {
            Port = _emailServiceCredentials.SmtpPort,
            Credentials = new NetworkCredential(_emailServiceCredentials.SenderEmail, _emailServiceCredentials.SenderPassword),
            EnableSsl = _emailServiceCredentials.EnableSsl,
        };
    }
    
    public Task Send(List<Email> emails, string subject, string content)
    {
        try
        {
            foreach (var email in emails)
            {
                var mailMessage = new MailMessage(_emailServiceCredentials.SenderEmail, email.Address, subject, content);
                mailMessage.IsBodyHtml = true;
                _smtpClient.Send(mailMessage);
            }

            Console.WriteLine("E-mails enviados com sucesso!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
        }

        return Task.CompletedTask;
    }
}