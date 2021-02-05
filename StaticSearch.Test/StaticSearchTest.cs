using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneApiApp.Models;

namespace OneApiApp.Test
{
    [TestClass]
    public class StaticSearchTest
    {
        [TestMethod]
        public async Task GoodInputTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("statisticsearch/update/5/5");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            Assert.IsTrue(data.Contains("1, 2, 3, 4, 5"));
        }

        [TestMethod]
        public async Task WrongInputTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("ee3");
            Assert.AreEqual(System.Net.HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [TestMethod]
        public async Task SameResponseInTimeOutTest()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var result = await client.GetAsync("statisticsearch/update/help/help");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, result.StatusCode);
            var content = result.Content;
            var data = await content.ReadAsStringAsync();
            var resp1 = JsonSerializer.Deserialize<IEnumerable<SearchResponse>>(data).ToList();
            result = await client.GetAsync("statisticsearch/update/help/help");
            content = result.Content;
            data = await content.ReadAsStringAsync();
            var resp2 = JsonSerializer.Deserialize<IEnumerable<SearchResponse>>(data).ToList();
            Assert.IsTrue(resp1.First().Date.Equals(resp2.First().Date));
        }

    }
}
