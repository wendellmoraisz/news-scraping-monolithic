using System.Net.Mail;
using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class Email : BaseEntity
{
    public string Address { get; set; }
}