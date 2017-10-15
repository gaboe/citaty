using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quotes.Data.Repositories.Channels;
using Quotes.Data.Repositories.Quotes;
using Quotes.Testing;
using Quotes.Testing.Infrastructure;
using System.Linq;

namespace Quotes.Tests.Data
{
    [TestClass]
    public class QuoteRepositoryTest
    {
        [TestMethod]
        public void GetAllQuotesTest()
        {
            using (var resolver = new TestResolver())
            {
                var quoteRepository = resolver.Resolve<IQuoteRepository>();
                var quotes = quoteRepository.GetAll().Result;

                Assert.IsNotNull(quotes);
                Assert.IsTrue(quotes.Count > 0);
            }
        }

        [TestMethod]
        public void GetTestingQuote()
        {
            using (var resolver = new TestResolver())
            {
                var quoteRepository = resolver.Resolve<IQuoteRepository>();
                var channelRepository = resolver.Resolve<IChannelRepository>();

                var channel = channelRepository.GetByTitle(TestingConstants.ChannelTitle).Result;

                var quotes = quoteRepository.GetQuotesByChannelID(channel.ID).Result;
                var testingQuote = quotes.First(x => x.Content.Equals(TestingConstants.QuoteContent));

                Assert.IsNotNull(quotes);
                Assert.IsTrue(quotes.Count > 0);
                Assert.IsNotNull(testingQuote);
            }
        }
    }
}