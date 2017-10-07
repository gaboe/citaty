using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Quotes.Data.Utils;
using Quotes.Domain;
using Quotes.Domain.Models;
using Quotes.Testing.Infrastructure;

namespace Quotes.Tests.Data
{
    [TestClass]
    public class SchemaNameProviderTest
    {
        [TestMethod]
        public void GetQuotesSchemaNameTest()
        {
            using (var resolver = new TestResolver())
            {
                var schemaProvider = resolver.Resolve<ISchemaNameProvider<Quote>>();
                var name = schemaProvider.GetSchemaName();
                Assert.AreEqual("quotes",name);
            }
        }

        [TestMethod]
        public void GetChannelsSchemaNameTest()
        {
            using (var resolver = new TestResolver())
            {
                var schemaProvider = resolver.Resolve<ISchemaNameProvider<Channel>>();
                var name = schemaProvider.GetSchemaName();
                Assert.AreEqual("channels", name);
            }
        }

        [TestMethod]
        public void GetBusSchemaNameTest()
        {
            using (var resolver = new TestResolver())
            {
                var schemaProvider = resolver.Resolve<ISchemaNameProvider<Bus>>();
                var name = schemaProvider.GetSchemaName();
                Assert.AreEqual("buses", name);
            }
        }

        [TestMethod]
        public void GetUserSchemaNameTest()
        {
            using (var resolver = new TestResolver())
            {
                var schemaProvider = resolver.Resolve<ISchemaNameProvider<User>>();
                var name = schemaProvider.GetSchemaName();
                Assert.AreEqual("users", name);
            }
        }

        private class Bus : IEntity<ObjectId>
        {
            public ObjectId ID { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateUpdated { get; set; }
        }
    }
}
