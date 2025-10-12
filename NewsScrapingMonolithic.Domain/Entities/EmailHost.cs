using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public class EmailHost : BaseEntity
{
    public Guid EmailId { get; set; }
    public Email Email { get; set; }
    
    public Guid HostId { get; set; }
    public Host Host { get; set; }
}