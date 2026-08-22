using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class NewsPage : BaseEntity
{
    public required string Url { get; set; }
    public required string HeaderHost { get; set; }
    public ICollection<Email> Emails { get; set; } = new List<Email>();
}
