using System;
using System.Collections.Generic;
using Autofac;

namespace Quotes.Core.Infrastructure
{
    public class DependencyConfigBase
    {
        public static IContainer Configure(IEnumerable<Module> modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }

            var container = builder.Build();

            return container;
        }

        public static IContainer Configure(Action<ContainerBuilder> customConfiguration, IEnumerable<Module> modules)
        {
            var builder = new ContainerBuilder();

            foreach (var module in modules)
            {
                builder.RegisterModule(module);
            }
            customConfiguration?.Invoke(builder);

            var container = builder.Build();

            return container;
        }

        public static IContainer Configure(Action<ContainerBuilder> customConfiguration)
        {
            var builder = new ContainerBuilder();
            customConfiguration?.Invoke(builder);
            var container = builder.Build();

            return container;
        }
    }
}