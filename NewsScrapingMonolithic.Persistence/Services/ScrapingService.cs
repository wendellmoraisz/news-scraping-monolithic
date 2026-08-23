using NewsScrapingMonolithic.Application.Services;
using HtmlAgilityPack;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Persistence.Services;

public class ScrapingService : IScrapingService
{
    public async Task<IEnumerable<NewsTitleDto>> ExtractNewsTitles(NewsPage newsPage)
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("host", newsPage.HeaderHost);

        var response = await httpClient.GetAsync(newsPage.Url);
        var pageHtml = await response.Content.ReadAsStringAsync();

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(pageHtml);

        var newsHeaders = htmlDocument.DocumentNode.SelectNodes("//h2[@class='tileHeadline']");
        if (newsHeaders == null) return new List<NewsTitleDto>();

        return newsHeaders
            .Select(header => header.SelectSingleNode("a"))
            .Where(a => a != null)
            .Select(a => new NewsTitleDto(a.InnerText, a.GetAttributeValue("href", "")))
            .ToList();
    }

    public async Task<string> GetDescription(string baseUrl, string host, string descUrl)
    {
        using var httpClient = new HttpClient();
        using var response = await httpClient.GetAsync(baseUrl + descUrl);
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
        SetHostUrlInImagesSrc(host, contentSection.Descendants("img"));
        SetHostUrlInLinksHref(host, contentSection.Descendants("a"));

        return contentSection.InnerHtml;
    }

    private static void RemoveNodes(HtmlNodeCollection nodesCollection)
    {
        foreach (var node in nodesCollection)
        {
            node.Remove();
        }
    }

    private void SetHostUrlInImagesSrc(string host, IEnumerable<HtmlNode> htmlImages)
    {
        foreach (var img in htmlImages)
        {
            var href = img.GetAttributeValue("src", "");
            img.SetAttributeValue("src", host + href);
        }
    }

    private void SetHostUrlInLinksHref(string host, IEnumerable<HtmlNode> htmlLinks)
    {
        foreach (var link in htmlLinks)
        {
            var href = link.GetAttributeValue("href", "");

            if (!IsValidUrl(href))
            {
                link.SetAttributeValue("href", host + href);
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
