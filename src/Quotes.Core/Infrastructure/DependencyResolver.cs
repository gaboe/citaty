using Autofac;
using System;
using System.Collections.Generic;

namespace Quotes.Core.Infrastructure
{
    public class DependencyResolver : IDisposable
    {
        private IContainer _container;
        private ILifetimeScope _scope;

        /// <summary>
        /// Send configuration, check for null params
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="customConfiguration"></param>
        public DependencyResolver(IEnumerable<Module> modules = null, Action<ContainerBuilder> customConfiguration = null)
        {
            Initialize(modules ?? new List<Module>(0), customConfiguration ?? (_ => { }));
        }

        protected void Initialize(IEnumerable<Module> modules, Action<ContainerBuilder> customConfiguration)
        {
            _container = Configure(modules, customConfiguration);
            _scope = _container.BeginLifetimeScope();
        }

        public IContainer Configure(IEnumerable<Module> customConfiguration, Action<ContainerBuilder> configuration)
        {
            var container = DependencyConfigBase.Configure(configuration, customConfiguration);

            return container;
        }

        public void Dispose()
        {
            _scope.Dispose();
            _container.Dispose();
        }

        public object GetService(Type type)
        {
            return _scope.Resolve(type);
        }

        public T GetService<T>() where T : class
        {
            return _scope.Resolve<T>();
        }

        public T Resolve<T>()
        {
            return _scope.Resolve<T>();
        }

        public ILifetimeScope GetCurrentLifetimeScope()
        {
            return _scope;
        }
    }
}
