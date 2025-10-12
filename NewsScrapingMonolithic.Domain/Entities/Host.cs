using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class Host : BaseEntity
{
    public string Address { get; set; }
    public IEnumerable<Email> Emails { get; set; }
}