using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Api;
using Quotes.Core.Services.Channels;
using Quotes.Testing;
using Quotes.Testing.Infrastructure;
using Quotes.Testing.Providers;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Tests.GraphQL.Mutations
{
    [TestClass]
    public class QuoteMutationsTest
    {
        private readonly HttpClient _client;

        public QuoteMutationsTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(AppSettingsProvider.GetConfigurationRoot()));
            _client = server.CreateClient();
            _client.DefaultRequestHeaders.Authorization = TestingUtils.GetTokenForTestingUser(_client);

        }

        [TestMethod]
        public async Task CreateQuoteApiTest()
        {
            using (var resolver = new TestResolver())
            {
                //Arrange
                var channnelService = resolver.Resolve<IChannelService>();
                var channel = channnelService.GetByTitle(TestingConstants.ChannelTitle).Result;

                var quoteName = $"IntegrationApi_Quote_{Guid.NewGuid()}";
                var query =
                    $"{{\"query\":\"mutation {{\\n  createQuote(input: {{content:\\\"{quoteName}\\\",owningChannelID:\\\"{channel.ChannelID}\\\"}}) {{\\n    owningChannelID,\\n    quoteID,\\n    content\\n  }}\\n}}\",\"variables\":null}}";

                var content = new StringContent(query, Encoding.UTF8, "application/json");

                //Action
                var response = await _client.PostAsync("/graphql", content);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();

                // Assert
                Assert.IsTrue(responseString.Contains(quoteName));
            }
        }
    }
}