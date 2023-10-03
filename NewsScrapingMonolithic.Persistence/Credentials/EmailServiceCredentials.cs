namespace NewsScrapingMonolithic.Persistence.Credentials;

public class EmailServiceCredentials
{
    public string SenderEmail { get; set; }
    public string SenderPassword { get; set; }
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public bool EnableSsl { get; set; }
}