using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Api;
using Quotes.Core.Creators;
using Quotes.GraphQL.Parsers;
using Quotes.GraphQL.Tree;
using Quotes.Testing;
using Quotes.Testing.Infrastructure;
using Quotes.Testing.Providers;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Quotes.GraphQL.Queries;

namespace Quotes.Tests.GraphQL.Queries
{
    [TestClass]
    public class UserQueryTest
    {
        private readonly HttpClient _client;

        public UserQueryTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseConfiguration(AppSettingsProvider.GetConfigurationRoot()));
            _client = server.CreateClient();
        }

        [TestMethod]
        public async Task GetAllUsersDetailsTest()
        {
            using (var resolver = new TestResolver())
            {
                //Arrange
                var parser = resolver.Resolve<IGraphQLParser>();
                var creator = resolver.Resolve<IQueryCreator>();

                var tree = new TreeNode
                {
                    Value = "users",
                    Childrens = new List<TreeNode>
                    {
                        new TreeNode {Value = "userID"},
                        new TreeNode {Value = "login"},
                        new TreeNode
                        {
                            Value = "favouriteChannels",
                            Childrens = new List<TreeNode>
                            {
                                new TreeNode {Value = "title"},
                                new TreeNode
                                {
                                    Value = "quotes",
                                    Childrens = new List<TreeNode>
                                    {
                                        new TreeNode {Value = "title"},
                                        new TreeNode {Value = "content"}
                                    }
                                }
                            }
                        }
                    }
                };
                //Action
                var query = parser.ParseTree(tree);

                var responseMessage = await _client.PostAsync("/graphql", creator.CreateQuery(query));
                responseMessage.EnsureSuccessStatusCode();

                var responseString = await responseMessage.Content.ReadAsStringAsync();

                //Assert
                Assert.IsTrue(responseString.Contains(TestingConstants.QuoteTitle));
            }
        }

        [TestMethod]
        public async Task GetUserByLoginTest()
        {
            //Arrange
            const string query = "{\"query\":\"{\\n  user(login: \\\"fictional.user\\\") {\\n    userID\\n    login\\n  }\\n}\\n\",\"variables\":null,\"operationName\":null}";

            var content = new StringContent(query, Encoding.UTF8, "application/json");

            //Action
            var response = await _client.PostAsync("/graphql", content);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.IsTrue(responseString.Contains(TestingConstants.UserLogin));
        }
    }
}