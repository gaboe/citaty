using Autofac;
using Quotes.Core.Infrastructure;
using Quotes.Data.Infrastructure;

namespace Quotes.Api.Infrastructure
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new DataModule());
        }
    }
}