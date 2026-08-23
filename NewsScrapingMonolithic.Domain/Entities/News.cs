using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class News : BaseEntity
{
    public required string Title { get; set; }
    public required string Content { get; set; }
    public Guid NewsPageId { get; set; }
    public NewsPage NewsPage { get; set; } = null!;
    public bool Sent { get; set; } = false;
}