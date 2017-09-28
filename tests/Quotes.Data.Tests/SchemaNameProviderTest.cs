using System;
using System.Dynamic;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Moq;
using Quotes.Data.Domain;
using Quotes.Data.Domain.Models;
using Quotes.Data.Utils;
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
        public void GetBusSchemaNameTest()
        {
            using (var resolver = new TestResolver())
            {
                var schemaProvider = resolver.Resolve<ISchemaNameProvider<Bus>>();
                var name = schemaProvider.GetSchemaName();
                Assert.AreEqual("buses", name);
            }
        }

        private class Bus : IEntity<ObjectId>
        {
            public ObjectId ID { get; set; }
        }
    }
}
