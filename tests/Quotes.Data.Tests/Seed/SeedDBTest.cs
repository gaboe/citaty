using System.Collections.Generic;
using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Data.Context;
using Quotes.Data.Domain.Models;
using Quotes.Data.Utils;
using Quotes.Testing;
using Quotes.Testing.Infrastructure;

namespace Quotes.Tests.Data.Seed
{
    //[TestClass]
    public class SeedDbTest
    {
        //[TestMethod]
        public void TruncateAndSeed()
        {
            //if (!IsSeedingEnabled())
            //    return;
            Truncate();
            Seed();
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
                    Content = new Faker().Lorem.Sentence(6, 6)
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
            var faker = new Faker();

            var quotes = new List<Quote>();
            for (var j = 0; j < i; j++)
            {
                quotes.Add(new Quote { Title = faker.Lorem.Slug(2), Content = faker.Lorem.Sentence(15, 4) });
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
