using Autofac;
using Microsoft.Extensions.Configuration;
using Quotes.Core.Infrastructure;
using Quotes.Data.Infrastructure;
using Quotes.Domain.Settings;
using Quotes.GraphQL.Infrastructure;
using Quotes.Testing.Providers;

namespace Quotes.Testing.Infrastructure
{
    public class TestingContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var configurationRoot = AppSettingsProvider.GetConfigurationRoot();
            var appConfig = configurationRoot.GetSection("App").Get<AppSettings>();

            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new DataModule(appConfig));
            builder.RegisterModule(new GraphQLModule());
            builder.RegisterModule(new TestingModule());
        }
    }
}