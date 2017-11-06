using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Api;
using Quotes.GraphQL.Creators;
using Quotes.GraphQL.Parsers;
using Quotes.Testing.Core;
using Quotes.Testing.Core.Infrastructure;
using Quotes.Testing.Core.Providers;
using System.Net.Http;
using System.Threading.Tasks;

namespace Quotes.Tests.GraphQL.Queries
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
            _client.DefaultRequestHeaders.Authorization = TestingUtils.GetTokenForTestingUser(_client);

        }

        [TestMethod]
        public async Task GetTestingQuoteTest()
        {
            using (var resolver = new TestResolver())
            {
                //Arrange
                var parser = resolver.Resolve<IGraphQLParser>();
                var creator = resolver.Resolve<IQueryCreator>();

                //Action
                var query = parser.Parse("query", "quote", new[] { "quoteID", "content" });

                var response = await _client.PostAsync("/graphql", creator.CreateQuery(query));
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                //Assert
                Assert.IsTrue(responseString.Contains(TestingConstants.QuoteContent));
            }
        }
    }
}