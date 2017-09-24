using Autofac;
using Citaty.Core.Infrastructure;
using Citaty.Data.Infrastructure;

namespace Citaty.Api.Infrastructure
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