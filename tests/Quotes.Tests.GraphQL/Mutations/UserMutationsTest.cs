using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Api;
using Quotes.Testing.Providers;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Quotes.Testing;

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
            var login = $"IntegrationApi_{Guid.NewGuid()}";
            var query =
                $"{{\"query\":\"mutation CreateUser{{\\n  createUser(login:\\\"{{{login}}}\\\"){{userID,login}}\\n}}\",\"variables\":null,\"operationName\":\"CreateUser\"}}\r\nName\r\n";

            var content = new StringContent(query, Encoding.UTF8, "application/json");

            //Action
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.IsTrue(responseString.Contains(login));
        }
    }
}