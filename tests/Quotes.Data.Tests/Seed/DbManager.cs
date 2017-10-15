using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using Quotes.Data.Context;
using Quotes.Data.Utils;
using Quotes.Domain.Models;
using Quotes.Testing;
using Quotes.Testing.Infrastructure;
using System;
using System.Collections.Generic;

namespace Quotes.Tests.Data.Seed
{
    [TestClass]
    public class DbManager
    {
        [AssemblyInitialize]
        public static void TruncateAndSeed(TestContext context)
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

                var channel = new Channel
                {
                    Title = TestingConstants.ChannelTitle,
                };

                connection.GetCollection<Channel>(channelSchema).InsertOne(channel);

                connection.GetCollection<Quote>(quoteSchema).InsertOne(new Quote
                {
                    Content = TestingConstants.QuoteContent,
                    ChannelID = channel.Id
                });

                SeedUser(channel);
                GetChannelQuotes(channel.Id, 1_000);
            }
        }

        private static void SeedUser(Channel channel)
        {
            using (var resolver = new TestResolver())
            {
                var channelSchema = resolver.Resolve<ISchemaNameProvider<Channel>>().GetSchemaName();
                var connection = resolver.Resolve<IDbConnectionFactory>().GetConnection();
                var userSchema = resolver.Resolve<ISchemaNameProvider<User>>().GetSchemaName();

                connection.DropCollection(userSchema);

                var favouriteChannel1 = new Channel { Title = $"Favourite channel 1" };
                var favouriteChannel2 = new Channel { Title = $"Favourite channel 2" };
                var favouriteChannel3 = new Channel { Title = $"Favourite channel 3" };
                connection.GetCollection<Channel>(channelSchema)
                    .InsertMany(new List<Channel>
                    {
                        favouriteChannel1,
                        favouriteChannel2,
                        favouriteChannel3
                    });

                connection.GetCollection<User>(userSchema).InsertOne(new User
                {
                    Login = TestingConstants.UserLogin,
                    FavouriteChannels = new List<Channel>
                    {
                        channel,
                        favouriteChannel1,
                        favouriteChannel2,
                        favouriteChannel3
                    }
                });

                GetChannelQuotes(favouriteChannel1.Id, 1_000);
                GetChannelQuotes(favouriteChannel2.Id, 100);
                GetChannelQuotes(favouriteChannel3.Id, 10);
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