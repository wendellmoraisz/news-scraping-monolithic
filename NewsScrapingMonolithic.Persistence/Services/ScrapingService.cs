using System.Diagnostics;
using NewsScrapingMonolithic.Application.Services;
using HtmlAgilityPack;
using NewsScrapingMonolithic.Domain.Entities;

namespace NewsScrapingMonolithic.Persistence.Services;

public class ScrapingService : IScrapingService
{
    public async Task<IEnumerable<News>> ExtractNews(string baseUrl, string host)
    {
        var newsUrl = $"{baseUrl}/ultimas-noticias";
        
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("host", host);
        
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
                Content = await GetDescriptionAsync(baseUrl, host, newsHeader.SelectSingleNode("a").GetAttributeValue("href", ""))
            };
            newsList.Add(news);
        }

        return newsList;
    }

    private async Task<string> GetDescriptionAsync(string baseUrl, string host, string descUrl)
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
