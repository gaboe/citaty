using System;
using System.IO;
using System.Reflection;
using Autofac;
using Microsoft.Extensions.Configuration;
using Quotes.Data.Domain.Settings;
using Quotes.Data.Infrastructure;
using Module = Autofac.Module;

namespace Quotes.Testing.Infrastructure
{
    public class TestingContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var b = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath("..\\..\\..\\..\\..\\src\\Citaty.Web"))
                .AddJsonFile($"appsettings.{environment}.json")
                .AddEnvironmentVariables();
            var c = b.Build();  
            var appConfig = c.GetSection("App").Get<AppSettings>();

            builder.RegisterModule(new DataModule(appConfig));
            builder.RegisterModule(new TestingModule());
        }
    }
}
