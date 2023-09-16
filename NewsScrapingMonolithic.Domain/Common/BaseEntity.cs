namespace NewsScrapingMonolithic.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt;
    public DateTime UpdatedAt;
}