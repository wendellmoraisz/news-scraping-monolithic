using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class News : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
}