using System.Net.Mail;
using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class Email : BaseEntity
{
    private string Address { get; set; }

    public Email(string emailAddress)
    {
        if (!IsValid(emailAddress))
            throw new ArgumentException("E-mail inválido");
        Address = emailAddress;
    }

    private bool IsValid(string emailAddress)
    {
        try
        {
            var email = new MailAddress(emailAddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}