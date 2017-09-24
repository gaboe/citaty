using Autofac;
using Citaty.Core.Infrastructure;

namespace Citaty.Api.Infrastructure
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModule());
        }
    }
}