using NewsScrapingMonolithic.Application.Services;
using HtmlAgilityPack;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Persistence.Services;

public class ScrapingService : IScrapingService
{
    private const string BaseUrl = "https://altamira.ifpa.edu.br";
    private const string Host = "altamira.ifpa.edu.br";

    public async Task<IEnumerable<News>> ExtractNews()
    {
        const string newsUrl = $"{BaseUrl}/ultimas-noticias";
        
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("host", Host);
        
        var response = await httpClient.GetAsync(newsUrl);
        var pageHtml = await response.Content.ReadAsStringAsync();

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(pageHtml);
        
        var newsHeaders = htmlDocument.DocumentNode.SelectNodes("//h2[@class='tileHeadline']");
        var newsList = new List<News>();

        if (newsHeaders == null) return newsList;
        foreach (var newsHeader in newsHeaders)
        {
            var news = new News
            {
                Title = newsHeader.SelectSingleNode("a").InnerText,
                Content = await GetDescriptionAsync(newsHeader.SelectSingleNode("a").GetAttributeValue("href", ""))
            };
            newsList.Add(news);
        }

        return newsList;
    }

    private static async Task<string> GetDescriptionAsync(string descUrl)
    {
        using var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync(BaseUrl + descUrl);
        var pageContent = await response.Content.ReadAsStringAsync();
        
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(pageContent);

        var contentSection = htmlDocument.DocumentNode.SelectSingleNode("//div[@class='item-page']");

        if (contentSection == null) return string.Empty;
        
        var nodesToRemove = contentSection.SelectNodes(
            "//div[contains(@class, 'content-header-options-1')]" +
            "| //span[contains(@class, documentCategory)]" +
            "| //h1[contains(@class, 'secondaryHeading')]"
        );
            
        RemoveNodes(nodesToRemove);
        SetHostUrlInImagesSrc(contentSection.Descendants("img"));
        SetHostUrlInLinksHref(contentSection.Descendants("a"));
        
        return contentSection.InnerHtml;
    }

    private static void RemoveNodes(HtmlNodeCollection nodesCollection)
    {
        foreach (var node in nodesCollection)
        {
            node.Remove();
        }
    }

    private static void SetHostUrlInImagesSrc(IEnumerable<HtmlNode> htmlImages)
    {
        foreach (var img in htmlImages)
        {
            var href = img.GetAttributeValue("src", "");
            img.SetAttributeValue("src", Host + href);
        }
    }

    private static void SetHostUrlInLinksHref(IEnumerable<HtmlNode> htmlLinks)
    {
        foreach (var link in htmlLinks)
        {
            var href = link.GetAttributeValue("href", "");

            if (!IsValidUrl(href))
            {
                link.SetAttributeValue("href", Host + href);
            }
        }
    }

    private static bool IsValidUrl(string url)
    {
        if (Uri.TryCreate(url, UriKind.Absolute, out var resultUri))
        {
            return resultUri.Scheme == Uri.UriSchemeHttp || resultUri.Scheme == Uri.UriSchemeHttps;
        }

        return false;
    }
}