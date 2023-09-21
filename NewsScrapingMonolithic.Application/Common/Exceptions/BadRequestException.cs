namespace NewsScrapingMonolithic.Application.Common.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
    }

    public BadRequestException(string[] errors) : base(string.Join(";", errors))
    {
        Errors = errors;
    }
    
    public string[] Errors { get; set; }
}