using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Api;
using Quotes.Testing.Core;
using Quotes.Testing.Core.Providers;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Quotes.Tests.GraphQL.Mutations
{
    [TestClass]
    public class UserMutationsTest
    {
        private readonly HttpClient _client;

        public UserMutationsTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(AppSettingsProvider.GetConfigurationRoot()));
            _client = server.CreateClient();
            _client.DefaultRequestHeaders.Authorization = TestingUtils.GetTokenForTestingUser(_client);
        }

        [TestMethod]
        public async Task CreateUserApiTest()
        {
            //Arrange
            var username = $"integration.test.{Guid.NewGuid()}";
            var query =
                $"{{\"query\":\"mutation{{\\n  createUser(username:\\\"{username}\\\",password:\\\"#Aa123456789\\\"){{\\n    userID\\n    userName\\n  }}\\n}}\",\"variables\":null}}\r\nName\r\n";
            var content = new StringContent(query, Encoding.UTF8, "application/json");

            //Action
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.IsTrue(responseString.Contains(username));
        }
    }
}