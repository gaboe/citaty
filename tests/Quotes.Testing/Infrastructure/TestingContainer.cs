using Autofac;
using Microsoft.Extensions.Configuration;
using Quotes.Data.Infrastructure;
using System;
using System.IO;
using Quotes.Domain.Settings;

namespace Quotes.Testing.Infrastructure
{
    public class TestingContainer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath("..\\..\\..\\..\\..\\src\\Quotes.Api"))
                .AddJsonFile($"appsettings.{environment}.json")
                .AddEnvironmentVariables();
            var configurationRoot = configurationBuilder.Build();  
            var appConfig = configurationRoot.GetSection("App").Get<AppSettings>();

            builder.RegisterModule(new DataModule(appConfig));
            builder.RegisterModule(new TestingModule());
        }
    }
}
