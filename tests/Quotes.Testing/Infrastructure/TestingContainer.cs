using Autofac;
using Microsoft.Extensions.Configuration;
using Quotes.Core.Infrastructure;
using Quotes.Data.Infrastructure;
using Quotes.Domain.Settings;
using Quotes.GraphQL.Infrastructure;
using System;
using System.IO;

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

            builder.RegisterModule(new CoreModule());
            builder.RegisterModule(new DataModule(appConfig));
            builder.RegisterModule(new GraphQLModule());
            builder.RegisterModule(new TestingModule());
        }
    }
}