using Autofac;

namespace Quotes.Testing.Core.Infrastructure
{
    public class TestingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterGeneric(typeof(DummyLoggerService<>)).As(typeof(ILogger<>));
        }
    }
}