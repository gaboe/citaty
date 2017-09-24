using Autofac;
using Quotes.Data.Context;
using Quotes.Data.Queries;
using Quotes.Data.Repositories.Quotes;

namespace Quotes.Data.Infrastructure
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<QuoteRepository>().As<IQuoteRepository>();
            builder.RegisterType<QuotesContextProvider>().As<IQuotesContextProvider>();

            builder.RegisterType<QuoteQuery>();
        }
    }
}