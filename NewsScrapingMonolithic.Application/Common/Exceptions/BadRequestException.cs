namespace NewsScrapingMonolithic.Application.Common;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string[] errors) : base("Muitos erros ocorreram. Veja os detalhes")
    {
        Errors = errors;
    }
    
    public string[] Errors { get; set; }
}