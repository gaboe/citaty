using Autofac;
using Citaty.Core.Services;

namespace Citaty.Core.Infrastructure
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ValueService>().As<IValueService>();
        }
    }
}
