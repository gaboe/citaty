using Autofac;
using Microsoft.Extensions.Logging;
using Quotes.Testing.Dummy;

namespace Quotes.Testing.Infrastructure
{
    public class TestingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(DummyLoggerService<>)).As(typeof(ILogger<>));
        }
    }
}
