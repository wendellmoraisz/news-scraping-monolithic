namespace NewsScrapingMonolithic.Application.Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string error) : base(error)
    {
    }
    
    public ConflictException(string[] errors) : base(string.Join(";", errors))
    {
    }
}