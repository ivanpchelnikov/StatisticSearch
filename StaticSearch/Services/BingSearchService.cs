using System;
using System.Net.Http;
using System.Threading.Tasks;
using OneApiApp.Models;
using OneApiApp.Services.Interface;

namespace OneApiApp.Services
{
    public class BingSearchService : StatisticSearchService, IBingSearchService
    {
        public const string _searchEngines = @"https://bing.com";
        public const string _tagSearch = "//li[contains(@class, 'b_algo')]";
        public const int triesAttempt = 3;

        public BingSearchService(HttpClient httpClient) : base(httpClient, _tagSearch)
        {
        }
        public string SearchName => _searchEngines;
        public async Task<SearchResult> Search(string keywords, string tagUrl)
        {
            var returnResult = new SearchResult { Keywords = keywords, Url = tagUrl };
            var searchUrl = $"{_searchEngines}/search?q={keywords}&count=100";
            string res = string.Empty;
            for (int t = 0; t < triesAttempt; t++)
            {
                res = await base.SearchByUrl(searchUrl, tagUrl);
                if (!res.Equals("0")) break;
            }

            returnResult.Results =  res;
            return returnResult;
        }
    }
}
