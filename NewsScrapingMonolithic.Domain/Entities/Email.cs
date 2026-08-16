using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class Email : BaseEntity
{
    public required string Address { get; set; }
    public ICollection<Host> Hosts { get; set; } = new List<Host>();
}