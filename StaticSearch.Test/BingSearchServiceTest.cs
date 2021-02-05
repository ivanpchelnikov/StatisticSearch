using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneApiApp.Services;

namespace OneApiApp.Test
{
    [TestClass]

    public class BingSearchServiceTest
    {
        [TestMethod]
        public void TagSearchTest()
        {
            Assert.IsTrue(BingSearchService._tagSearch.Equals("//li[contains(@class, 'b_algo')]"));
        }

        [TestMethod]
        public void InheritTest()
        {
            var client = new HttpClient();
            var bingService = new BingSearchService(client);
            Assert.IsTrue(bingService is StatisticSearchService);
        }

        [TestMethod]
        public async Task BingSearchTest()
        {
            var client = new HttpClient();
            var bingService = new BingSearchService(client);
            var result = await bingService.Search("5", "5");
            Assert.IsTrue(result.Results.Contains("1, 2, 3, 4, 5"));
        }
    }
}
