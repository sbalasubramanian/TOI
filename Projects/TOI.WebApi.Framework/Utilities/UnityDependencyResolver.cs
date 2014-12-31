using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace TOI.WebApi.Framework.Utilities
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _dependencyContainer;

        public UnityDependencyResolver(IUnityContainer dependencyContainer)
        {
            _dependencyContainer = dependencyContainer;
        }

        public void Dispose()
        {
            _dependencyContainer.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _dependencyContainer.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _dependencyContainer.ResolveAll(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IDependencyScope BeginScope()
        {
            return new UnityDependencyResolver(_dependencyContainer);
        }
    }
}
