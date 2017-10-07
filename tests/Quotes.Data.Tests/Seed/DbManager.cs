using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Data.Context;
using Quotes.Data.Utils;
using Quotes.Domain.Models;
using Quotes.Testing;
using Quotes.Testing.Infrastructure;
using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Quotes.Tests.Data.Seed
{
    [TestClass]
    public class DbManager
    {
        [AssemblyInitialize]
        public static void TruncateAndSeed(TestContext context)
        {
            if (!IsSeedingEnabled())
                return;
            Truncate();
            Seed();
        }

        private static void Seed()
        {
            using (var resolver = new TestResolver())
            {
                var quoteSchema = resolver.Resolve<ISchemaNameProvider<Quote>>().GetSchemaName();
                var channelSchema = resolver.Resolve<ISchemaNameProvider<Channel>>().GetSchemaName();
                var userSchema = resolver.Resolve<ISchemaNameProvider<User>>().GetSchemaName();

                var connection = resolver.Resolve<IDbConnectionFactory>().GetConnection();
                connection.DropCollection(quoteSchema);
                connection.DropCollection(channelSchema);
                connection.DropCollection(userSchema);

                connection.GetCollection<Quote>(quoteSchema).InsertOne(new Quote
                {
                    Title = TestingConstants.QuoteName,
                    Content = new Faker().Lorem.Sentence(6, 6)
                });

                var channel = new Channel
                {
                    Title = TestingConstants.ChannelTitle,
                };

                connection.GetCollection<User>(userSchema).InsertOne(new User{Login = TestingConstants.UserLogin});
                connection.GetCollection<Channel>(channelSchema).InsertOne(channel);
                GetChannelQuotes(channel.ID, 1_000);
            }
        }

        private static IEnumerable<Quote> GetChannelQuotes(ObjectId channelId, int count)
        {
            var faker = new Faker();

            var quotes = new List<Quote>();
            for (var j = 0; j < count; j++)
            {
                quotes.Add(new Quote
                {
                    ChannelID = channelId,
                    Title = faker.Lorem.Slug(2),
                    Content = faker.Lorem.Sentence(15, 4)
                });
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

        private static bool IsSeedingEnabled()
        {
            var environmentVariable = Environment.GetEnvironmentVariable("DB_SEEDING_ENABLED");
            bool.TryParse(environmentVariable, out var isEnabled);
            return isEnabled;
        }
    }
}