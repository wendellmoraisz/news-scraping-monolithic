using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class Host : BaseEntity
{
    public required string Address { get; set; }
    public required IEnumerable<Email> Emails { get; set; }
}