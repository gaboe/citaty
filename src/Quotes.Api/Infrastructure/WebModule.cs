using Autofac;
using GraphQL;
using Quotes.Core.Infrastructure;
using Quotes.Data.Infrastructure;
using Quotes.Domain.Settings;
using Quotes.GraphQL.Infrastructure;

namespace Quotes.Api.Infrastructure
{
    public class WebModule : Module
    {
        private readonly AppSettings _appConfig;

        public WebModule(AppSettings appConfig)
        {
            _appConfig = appConfig;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new DataModule(_appConfig));
            builder.RegisterModule(new GraphQLModule());

            builder.RegisterType<DocumentExecuter>().As<IDocumentExecuter>();
            builder.RegisterType<SchemaProvider>().As<ISchemaProvider>();
        }
    }
}