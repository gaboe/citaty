using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.GraphQL.Parsers;
using Quotes.GraphQL.Tree;
using Quotes.Testing.Core.Infrastructure;
using System.Collections.Generic;

namespace Quotes.Tests.GraphQL.Parsers
{
    [TestClass]
    public class TreeParserTest
    {
        [TestMethod]
        public void ParseTwoLevelsTreeTest()
        {
            using (var resolver = new TestResolver())
            {
                var parser = resolver.Resolve<IGraphQLParser>();
                var tree = new TreeNode
                {
                    Value = "users",
                    Childrens = new List<TreeNode>
                    {
                        new TreeNode {Value = "userID"},
                        new TreeNode {Value = "login"},
                    }
                };
                var result = parser.ParseTree(tree);

                Assert.AreEqual("{users{userID,login}}", result);
            }
        }

        [TestMethod]
        public void ParseThreeLevelsTreeTest()
        {
            using (var resolver = new TestResolver())
            {
                var parser = resolver.Resolve<IGraphQLParser>();
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
                                new TreeNode {Value = "id"},
                                new TreeNode {Value = "title"}
                            }
                        }
                    }
                };
                var result = parser.ParseTree(tree);

                Assert.AreEqual("{users{userID,login,favouriteChannels{id,title}}}", result);
            }
        }

        [TestMethod]
        public void ParseFourLevelsTreeTest()
        {
            using (var resolver = new TestResolver())
            {
                var parser = resolver.Resolve<IGraphQLParser>();
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
                                new TreeNode {Value = "id"},
                                new TreeNode {Value = "title"},
                                new TreeNode
                                {
                                    Value = "quotes",
                                    Childrens = new List<TreeNode>
                                    {
                                        new TreeNode {Value = "id"},
                                        new TreeNode {Value = "title"},
                                        new TreeNode {Value = "content"}
                                    }
                                }
                            }
                        }
                    }
                };
                var result = parser.ParseTree(tree);

                Assert.AreEqual("{users{userID,login,favouriteChannels{id,title,quotes{id,title,content}}}}", result);
            }
        }

        [TestMethod]
        public void ParseFiveLevelsTreeTest()
        {
            using (var resolver = new TestResolver())
            {
                var parser = resolver.Resolve<IGraphQLParser>();
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
                                new TreeNode {Value = "id"},
                                new TreeNode {Value = "title"},
                                new TreeNode
                                {
                                    Value = "quotes",
                                    Childrens = new List<TreeNode>
                                    {
                                        new TreeNode {Value = "id"},
                                        new TreeNode {Value = "title"},
                                        new TreeNode {Value = "content"},
                                        new TreeNode
                                        {
                                            Value = "users",
                                            Childrens = new List<TreeNode>
                                            {
                                                new TreeNode {Value = "id"},
                                                new TreeNode {Value = "login"}
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
                var result = parser.ParseTree(tree);

                Assert.AreEqual(
                    "{users{userID,login,favouriteChannels{id,title,quotes{id,title,content,users{id,login}}}}}",
                    result);
            }
        }
    }
}