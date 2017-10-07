using Autofac;
using Quotes.Data.Context;
using Quotes.Data.Repositories.Channels;
using Quotes.Data.Repositories.Quotes;
using Quotes.Data.Repositories.Users;
using Quotes.Data.Utils;
using Quotes.Domain.Settings;

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
                .WithParameter("databaseSettings", _appConfig.DatabaseSettings);

            builder.RegisterType<QuoteRepository>().As<IQuoteRepository>();
            builder.RegisterType<ChannelRepository>().As<IChannelRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
        }
    }
}