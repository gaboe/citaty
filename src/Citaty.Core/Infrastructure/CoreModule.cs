using Autofac;
using Citaty.Core.Services;

namespace Quotes.Core.Infrastructure
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ValueService>().As<IValueService>();
        }
    }
}
