using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using OneApiApp.Services.Interface;

namespace OneApiApp.Services
{
    public abstract class StatisticSearchService : IStatisticSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly string _tagSearch;

        public StatisticSearchService(HttpClient httpClient, string tagSearch)
        {
            _tagSearch = tagSearch;
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public virtual async Task<string> SearchByUrl(string searchUrl, string tagUrl)
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");

            var returnResult = new StringBuilder();

            using HttpResponseMessage response = await _httpClient.GetAsync(searchUrl);
            using (var body = response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(body.Result))
            {
                var html = await reader.ReadToEndAsync();
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var htmlNodes = htmlDoc.DocumentNode.SelectNodes(_tagSearch);
                if (htmlNodes != null)
                {
                    for (int i = 0; i < htmlNodes.Count; i++)
                    {
                        var node = htmlNodes[i];
                        if (node.InnerText.Contains(tagUrl, StringComparison.OrdinalIgnoreCase))
                        {
                            returnResult.Append(string.IsNullOrEmpty(returnResult.ToString()) ? $"{i + 1}" : $", {i + 1}");
                        }
                    }
                }
            }
            return string.IsNullOrEmpty(returnResult.ToString()) ? "0" : returnResult.ToString();
        }
    }
}
