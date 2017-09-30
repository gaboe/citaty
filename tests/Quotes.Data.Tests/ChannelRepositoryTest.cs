using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Data.Domain.Models;
using Quotes.Data.Repositories.Channels;
using Quotes.Testing.Infrastructure;

namespace Quotes.Tests.Data
{
    [TestClass]
    public class ChannelRepositoryTest
    {
        [TestMethod]
        public void InsertChannelTest()
        {
            using (var resolver = new TestResolver())
            {
                var channelRepository = resolver.Resolve<IChannelRepository>();
                var title = $"Integračné citáty číslo {Guid.NewGuid()}";

                channelRepository.Add(new Channel {Title = title});

                var channel = channelRepository.GetByTitle(title).Result;

                Assert.IsNotNull(channel);
                Assert.AreEqual(title, channel.Title);
            }
        }

        [TestMethod]
        public void AddQuoteTest()
        {
            using (var resolver = new TestResolver())
            {
                var channelRepository = resolver.Resolve<IChannelRepository>();
                var quoteContent = $"Toto je integračný citát číslo: {Guid.NewGuid()}";
                var qouteTitle = $"{Guid.NewGuid()}";

                var quote = new Quote
                {
                    Content = quoteContent,
                    Title = qouteTitle
                };
                var channelTitle = $"Integračné citáty číslo {Guid.NewGuid()}";

                channelRepository.Add(
                    new Channel
                    {
                        Title = channelTitle,
                        Quotes = new List<Quote> {quote}
                    });

                var channel = channelRepository.GetByTitle(channelTitle).Result;
                Assert.IsNotNull(channel);
                Assert.AreEqual(channelTitle, channel.Title);
                Assert.AreEqual(1, channel.Quotes.Count());
                Assert.AreEqual(qouteTitle, quote.Title);
                Assert.AreEqual(quoteContent, quote.Content);
            }
        }
    }
}