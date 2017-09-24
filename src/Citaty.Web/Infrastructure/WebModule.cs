using Autofac;
using Citaty.Core.Infrastructure;

namespace Citaty.Web.Infrastructure
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModule());
        }
    }
}