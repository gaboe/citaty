using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Data.Context;
using Quotes.Data.Domain.Models;
using Quotes.Data.Utils;
using Quotes.Testing;
using Quotes.Testing.Infrastructure;
using System;
using System.Collections.Generic;
using Bogus;

namespace Quotes.Seed
{
    [TestClass]
    public class SeedDb
    {
        [TestMethod]
        public void TruncateAndSeed()
        {
            if (!IsSeedingEnabled())
                return;
            Truncate();
            Seed();
        }

        private static bool IsSeedingEnabled()
        {
            var environmentVariable = Environment.GetEnvironmentVariable("DB_SEEDING_ENABLED");
            bool.TryParse(environmentVariable, out var isEnabled);
            return isEnabled;
        }

        private static void Seed()
        {
            using (var resolver = new TestResolver())
            {
                var quoteSchema = resolver.Resolve<ISchemaNameProvider<Quote>>().GetSchemaName();
                var channelSchema = resolver.Resolve<ISchemaNameProvider<Channel>>().GetSchemaName();
                var connection = resolver.Resolve<IDbConnectionFactory>().GetConnection();
                connection.DropCollection(quoteSchema);
                connection.DropCollection(channelSchema);

                connection.GetCollection<Quote>(quoteSchema).InsertOne(new Quote
                {
                    Title = TestingConstants.QuoteName,
                    Content = Guid.NewGuid().ToString()
                });

                connection.GetCollection<Channel>(channelSchema).InsertOne(new Channel
                {
                    Title = TestingConstants.ChannelTitle,
                    Quotes = GetChannelQuotes(1_000)
                });
            }
        }

        private static IEnumerable<Quote> GetChannelQuotes(int i)
        {
            var faker = new Faker("cz");

            var quotes = new List<Quote>();
            for (var j = 0; j < i; j++)
            {
                quotes.Add(new Quote {Title = faker.Lorem.Sentence(15, 4), Content = Guid.NewGuid().ToString()});
            }
            using (var resolver = new TestResolver())
            {
                var quoteSchema = resolver.Resolve<ISchemaNameProvider<Quote>>().GetSchemaName();
                var connection = resolver.Resolve<IDbConnectionFactory>().GetConnection();
                connection.GetCollection<Quote>(quoteSchema).InsertMany(quotes);
            }
            return quotes;
        }

        private static void Truncate()
        {
            using (var resolver = new TestResolver())
            {
                var connection = resolver.Resolve<IDbConnectionFactory>().GetConnection();
                connection.DropCollection(resolver.Resolve<ISchemaNameProvider<Quote>>().GetSchemaName());
                connection.DropCollection(resolver.Resolve<ISchemaNameProvider<Channel>>().GetSchemaName());
            }
        }
    }
}