using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Api;
using Quotes.Testing.Providers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quotes.Testing;

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
            _client.DefaultRequestHeaders.Authorization = TestingUtils.GetTokenForTestingUser(_client);
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

        [TestMethod]
        public void ParseResponse()
        {
            var response =
                "\"{\\r\\n  \\\"access_token\\\": \\\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJmaWN0aW9uYWwudXNlciIsImp0aSI6Ijc3Y2ZhODk1LWUxNDYtNDA3ZS1hYTg4LTYxYzc3MDhlMzI3MCIsImlhdCI6MTUwOTkwNjA3MCwibmJmIjoxNTA5OTA2MDcwLCJleHAiOjE1MDk5OTI0NzAsImlzcyI6IkRldlF1b3Rlc0FQSSIsImF1ZCI6IkRldlF1b3Rlc0NsaWVudCJ9.ntNk6fO2QpRwyac-6pLaA21nqmrmSicD2hpH2btWFYM\\\",\\r\\n  \\\"expires_in\\\": \\\"2017-11-06T18:21:10.9235244Z\\\"\\r\\n}\"\r\n";

            JObject obj = JObject.Parse(response);


            var indexOf = response.LastIndexOf("\\\"access_token\\\":", StringComparison.Ordinal);
        }
    }
}