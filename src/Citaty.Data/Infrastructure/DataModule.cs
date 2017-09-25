using Autofac;
using Quotes.Data.Context;
using Quotes.Data.Queries;
using Quotes.Data.Repositories.Quotes;
using Quotes.Data.Utils;

namespace Quotes.Data.Infrastructure
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(BaseContextProvider<>)).As(typeof(IBaseContextProvider<>));
            builder.RegisterGeneric(typeof(SchemaNameProvider<>)).As(typeof(ISchemaNameProvider<>));

            builder.RegisterType<QuoteRepository>().As<IQuoteRepository>();

            builder.RegisterType<QuotesContextProvider>().As<IQuotesContextProvider>();

            builder.RegisterType<QuoteQuery>();
        }
    }
}