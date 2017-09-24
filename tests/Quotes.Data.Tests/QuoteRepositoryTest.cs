using Autofac;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Quotes.Data.Repositories.Quotes;
using Quotes.Testing.Infrastructure;

namespace Quotes.Tests.Data
{
    [TestClass]
    public class QuoteRepositoryTest
    {
        //[TestMethod]
        public void GetQuoteTest()
        {
            using (var resolver = new TestResolver())
            {
                var quoteRepository = resolver.Resolve<IQuoteRepository>();
                var quotes = quoteRepository.GetAll().Result;

                Assert.IsNotNull(quotes);
                Assert.IsTrue(quotes.Count > 0);
            }
        }
    }
}
