namespace NewsScrapingMonolithic.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt = DateTime.Now;
    public DateTime UpdatedAt;
}