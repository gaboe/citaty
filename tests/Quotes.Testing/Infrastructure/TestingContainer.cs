using Autofac;
using Microsoft.Extensions.Configuration;
using Quotes.Data.Domain.Settings;
using Quotes.Data.Infrastructure;

namespace Quotes.Testing.Infrastructure
{
    public class TestingContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            var b = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
            var c = b.Build();
            var appConfig = c.GetSection("App").Get<AppSettings>();

            builder.RegisterModule(new DataModule(appConfig));
            builder.RegisterModule(new TestingModule());
        }
    }
}
