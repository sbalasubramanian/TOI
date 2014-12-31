using System;
using System.Web.Http.Dependencies;

namespace TOI.WebApi.Framework.Utilities {
    internal static class DependencyResolverExtensions {
        public static TService Resolve<TService>(this IDependencyResolver servicesContainer) where TService : class
        {

            var service = servicesContainer.GetService(typeof(TService)) as TService;
            return service;
        }

        private static TService GetServiceOrThrow<TService>(this IDependencyResolver servicesContainer, Type clrType) where TService : class
        {
            clrType = clrType ?? typeof(TService);

            // get by DI
            var service = servicesContainer.GetService(typeof(TService)) as TService;
            if (service == null)
            {

                service = servicesContainer.GetService(clrType) as TService;

                if (service == null)
                {
                    try
                    {
                        service = Activator.CreateInstance(clrType) as TService;
                    }
                    catch (Exception)
                    {
                        throw new InvalidOperationException(
                            String.Format(ExceptionResources.DependencyContainerNotConfigured, clrType.FullName));
                    }
                }
            }

            return service;
        }
    }
}