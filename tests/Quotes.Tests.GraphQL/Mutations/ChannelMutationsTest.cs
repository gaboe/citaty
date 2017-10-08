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
    public class ChannelMutationsTest
    {
        private readonly HttpClient _client;

        public ChannelMutationsTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(AppSettingsProvider.GetConfigurationRoot()));
            _client = server.CreateClient();
        }

        [TestMethod]
        public async Task CreateChannelApiTest()
        {
            //Arrange
            var channelTitle = $"IntegrationApi_Channel_{Guid.NewGuid()}";
            var query =
                $"{{\"query\":\"mutation CreateChannel{{\\n  createChannel(title:\\\"{channelTitle}\\\"){{\\n    title,\\n    channelID,\\n    quotes {{\\n      owningChannelID\\n    }}\\n  }}\\n}}\",\"variables\":null,\"operationName\":\"CreateChannel\"}}";

            var content = new StringContent(query, Encoding.UTF8, "application/json");

            //Action
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.IsTrue(responseString.Contains(channelTitle));
        }
    }
}