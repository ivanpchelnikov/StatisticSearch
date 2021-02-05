using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneApiApp.Services;

namespace OneApiApp.Test
{
    [TestClass]
    public class GoogleSearchServiceTest
    {
        [TestMethod]
        public void TagSearchTest()
        {
            Assert.IsTrue(GoogleSearchService._tagSearch.Equals("//div[contains(@class, 'tF2Cxc')]"));
        }

        [TestMethod]
        public void InheritTest()
        {
            var client = new HttpClient();
            var googleService = new GoogleSearchService(client);
            Assert.IsTrue(googleService is StatisticSearchService);
        }

        [TestMethod]
        public async Task GoogleSearchTest()
        {
            var client = new HttpClient();
            var googleService = new GoogleSearchService(client);
            var result = await googleService.Search("5","5");
            Assert.IsTrue(result.Results.Contains("1, 2, 3, 4, 5"));
        }
    }
}
