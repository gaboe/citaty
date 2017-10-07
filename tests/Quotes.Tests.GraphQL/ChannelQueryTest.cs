using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Api;
using Quotes.Testing;
using Quotes.Testing.Providers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Tests.GraphQL
{
    [TestClass]
    public class ChannelQueryTest
    {
        private readonly HttpClient _client;

        public ChannelQueryTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(AppSettingsProvider.GetConfigurationRoot()));
            _client = server.CreateClient();
        }

        [TestMethod]
        public async Task GetTestingQuoteTest()
        {
            //Arrange
            const string query = @"{""query"": ""query { quote { quoteID title } }""}";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            //Action
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
           
            // Assert
            Assert.IsTrue(responseString.Contains(TestingConstants.QuoteName));
        }
    }
}