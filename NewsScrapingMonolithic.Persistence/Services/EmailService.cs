using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using NewsScrapingMonolithic.Application.Services;
using NewsScrapingMonolithic.Domain.Entities;
using NewsScrapingMonolithic.Persistence.Credentials;

namespace NewsScrapingMonolithic.Persistence.Services;

public class EmailService : IEmailService
{
    private readonly EmailServiceCredentials _emailServiceCredentials;
    private readonly HttpClient _httpClient;

    public EmailService(HttpClient httpClient, IOptions<EmailServiceCredentials> options)
    {
        _emailServiceCredentials = options.Value;
        _httpClient = httpClient;
    }
    
    public async Task Send(List<Email> emails, string subject, string content)
    {
        if (emails.Count == 0)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_emailServiceCredentials.BrevoApiKey))
        {
            throw new InvalidOperationException("A chave da API da Brevo não foi configurada.");
        }

        var request = new HttpRequestMessage(HttpMethod.Post, "v3/smtp/email")
        {
            Content = JsonContent.Create(new
            {
                sender = new
                {
                    name = _emailServiceCredentials.SenderName,
                    email = _emailServiceCredentials.SenderEmail
                },
                to = emails.Select(email => new { email = email.Address }),
                subject,
                htmlContent = content
            })
        };

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Headers.Add("api-key", _emailServiceCredentials.BrevoApiKey);

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }
}
