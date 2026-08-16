using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class Host : BaseEntity
{
    public required string Address { get; set; }
    public ICollection<Email> Emails { get; set; } = new List<Email>();
}
