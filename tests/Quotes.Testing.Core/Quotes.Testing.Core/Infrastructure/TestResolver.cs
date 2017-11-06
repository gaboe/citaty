using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Quotes.Core.Infrastructure;

namespace Quotes.Testing.Core.Infrastructure
{
    public class TestResolver : DependencyResolver
    {
        public TestResolver(IEnumerable<Module> modules, Action<ContainerBuilder> customConfiguration)
        {
            var injectableModules = new List<Module> { new TestingContainer() };
            injectableModules.AddRange(modules);

            Initialize(injectableModules, customConfiguration);
        }

        public TestResolver(IEnumerable<Module> modules)
        {
            var injectableModules = new List<Module> { new TestingContainer() };
            if (modules != null) injectableModules.AddRange(modules);

            Initialize(injectableModules, _ => { });
        }

        public TestResolver(Action<ContainerBuilder> customConfiguration)
        {
            Initialize(new List<Module>(1) { new TestingContainer() }, customConfiguration);
        }

        public TestResolver()
        {
            Initialize(new List<Module>(1) { new TestingContainer() }, _ => { });
        }
    }
}
