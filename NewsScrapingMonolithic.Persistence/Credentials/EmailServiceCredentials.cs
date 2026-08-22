namespace NewsScrapingMonolithic.Persistence.Credentials;

public class EmailServiceCredentials
{
    public string SenderEmail { get; set; } = string.Empty;
    public string? SenderName { get; set; }
    public string BrevoApiKey { get; set; } = string.Empty;
}
