using NewsScrapingMonolithic.Domain.Common;

namespace NewsScrapingMonolithic.Domain.Entities;

public sealed class News : BaseEntity
{
    public string Title;
    public string Content;

    public News(string title, string content)
    {
        Title = title;
        Content = content;
    }
}