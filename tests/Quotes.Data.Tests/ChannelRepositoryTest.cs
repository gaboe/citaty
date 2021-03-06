﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Data.Repositories.Channels;
using Quotes.Data.Repositories.Quotes;
using Quotes.Domain.Models;
using Quotes.Testing.Core.Infrastructure;
using System;
using System.Linq;

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

                channelRepository.Add(new Channel { Title = title });

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
                //Arrange
                var channelRepository = resolver.Resolve<IChannelRepository>();
                var quoteService = resolver.Resolve<IQuoteRepository>();
                var channelTitle = $"Integračné citáty číslo {Guid.NewGuid()}";
                var quoteContent = $"Toto je integračný citát číslo: {Guid.NewGuid()}";

                //Action
                var channel = new Channel
                {
                    Title = channelTitle,
                };

                channelRepository.Add(channel);

                var quote = new Quote
                {
                    ChannelID = channel.Id,
                    Content = quoteContent,
                };
                quoteService.Add(quote);

                //Assert
                var channel2 = channelRepository.GetByTitle(channelTitle).Result;
                var channelQuote = quoteService.GetQuotesByChannelID(channel.Id).Result;
                Assert.IsNotNull(channel2);
                Assert.AreEqual(channelTitle, channel2.Title);
                Assert.AreEqual(1, channelQuote.Count);
                Assert.AreEqual(quoteContent, channelQuote.Single().Content);
            }
        }
    }
}