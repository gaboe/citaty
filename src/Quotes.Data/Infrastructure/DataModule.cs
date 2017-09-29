using Autofac;
using Quotes.Data.Context;
using Quotes.Data.Domain.Settings;
using Quotes.Data.Queries;
using Quotes.Data.Repositories.Quotes;
using Quotes.Data.Utils;

namespace Quotes.Data.Infrastructure
{
    public class DataModule : Module
    {
        private readonly AppSettings _appConfig;

        public DataModule(AppSettings appConfig)
        {
            _appConfig = appConfig;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(DbContextProvider<>)).As(typeof(IDbContextProvider<>));
            builder.RegisterGeneric(typeof(SchemaNameProvider<>)).As(typeof(ISchemaNameProvider<>));
            builder.RegisterType<DbConnectionFactory>()
                .As<IDbConnectionFactory>()
                .WithParameters(new[]
                {
                    new NamedParameter("connectionString", _appConfig.DatabaseSettings.ConnectionString),
                    new NamedParameter("databaseName", _appConfig.DatabaseSettings.DatabaseName)
                });

            builder.RegisterType<QuoteRepository>().As<IQuoteRepository>();

            builder.RegisterType<QuoteQuery>();
        }
    }
}