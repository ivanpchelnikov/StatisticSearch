using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OneApiApp.Models;
using OneApiApp.Services.Interface;

namespace OneApiApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticSearchController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<StatisticSearchController> _logger;
        private readonly IGoogleSearchService _googleSearchService;
        private readonly IBingSearchService _bingSearchService;
        private readonly IMemoryCache _memoryCache;

        public StatisticSearchController(ILogger<StatisticSearchController> logger,
                                         IConfiguration configuration,
                                         IMemoryCache memoryCache,
                                         IGoogleSearchService googleSearchService,
                                         IBingSearchService bingSearchService)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _googleSearchService = googleSearchService;
            _bingSearchService = bingSearchService;
            _configuration = configuration;
        }
        [HttpGet]
        public IEnumerable<SearchResponse> Get()
        {
            _logger.LogInformation(1, $"Getting latest Statistic");
            var response = new List<SearchResponse>();
            if (_memoryCache.TryGetValue(_configuration["AppSettings:api:cachKey"], out Dictionary<DateTime, List<SearchResponse>> _cacheEntry))
            {
                response = _cacheEntry[_cacheEntry.Keys.Max()];
            }
            return response;
        }

        [HttpGet]
        [Route("update/{keywords}/{tagurl}")]
        public async Task<IEnumerable<SearchResponse>> Get(string keywords, string tagurl)
        {
            _logger.LogInformation(1, $"Getting update Statistic");
            var _response = new List<SearchResponse>();
            var isUpdate = true;
            if (_memoryCache.TryGetValue(_configuration["AppSettings:api:cachKey"], out Dictionary<DateTime, List<SearchResponse>> _cacheEntry))
            {
                var temp = _cacheEntry.FirstOrDefault(d => d.Value.All(s => s.Keywords.Equals(keywords)));
                if (temp.Value != null)
                {
                    _response = temp.Value;
                    isUpdate = temp.Key.AddMinutes(_configuration.GetValue<int>("AppSettings:api:timeout", 60)) < DateTime.Now;
                }
            }
            _cacheEntry ??= new Dictionary<DateTime, List<SearchResponse>>();

            if (isUpdate)
            {
                var newResult = new SearchResponse
                {
                    Keywords = keywords,
                    Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"),
                    Google = (await _googleSearchService.Search(keywords, tagurl)).Results,
                    Bing = (await _bingSearchService.Search(keywords, tagurl)).Results
                };
                _response.Add(newResult);
                _cacheEntry.Add(DateTime.Now, _response);
                _memoryCache.Set(_configuration["AppSettings:api:cachKey"], _cacheEntry);
            }
            return _response;
        }
    }
}

