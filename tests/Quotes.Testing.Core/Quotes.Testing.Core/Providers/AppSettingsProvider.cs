using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Quotes.Testing.Core.Providers
{
    public class AppSettingsProvider
    {
        public static IConfigurationRoot GetConfigurationRoot()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.GetFullPath("..\\..\\..\\..\\..\\src\\Quotes.Api"))
                .AddJsonFile($"appsettings.{environment}.json");
            //.AddEnvironmentVariables();
            var configurationRoot = configurationBuilder.Build();
            return configurationRoot;
        }
    }
}