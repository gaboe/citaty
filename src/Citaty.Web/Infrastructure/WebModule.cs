using Autofac;
using Quotes.Core.Infrastructure;
using Quotes.Data.Domain.Settings;
using Quotes.Data.Infrastructure;

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
        }
    }
}