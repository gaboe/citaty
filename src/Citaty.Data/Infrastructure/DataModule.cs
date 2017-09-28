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
            builder.RegisterGeneric(typeof(DbContextProvider<>)).As(typeof(IDbContextProvider<>));
            builder.RegisterGeneric(typeof(SchemaNameProvider<>)).As(typeof(ISchemaNameProvider<>));
            builder.RegisterType<DbConnectionFactory>().As<IDbConnectionFactory>();

            builder.RegisterType<QuoteRepository>().As<IQuoteRepository>();

            builder.RegisterType<QuoteQuery>();
        }
    }
}