using System.Web.Http;
using System.Web.Http.Dispatcher;
using Microsoft.Practices.Unity;
using TOI.WebApi.Framework.Core;
using TOI.WebApi.Framework.Defaults;
using TOI.WebApi.Framework.Utilities;

namespace TOI.WebApi.Framework
{
    public abstract class VersionDetectorConfigurationBase
    {

        public virtual void Configure()
        {
            Config.DependencyResolver = new UnityDependencyResolver(Container);
            Config.Services.Replace(typeof(IHttpControllerSelector), new VersionedApiControllerSelector(GlobalConfiguration.Configuration));

            Container.RegisterType<IControllerVersionDetector, ControllerVersionDetector>();
            Container.RegisterType<IControllerInformationDetector, ControllerInformationDetector>(new InjectionConstructor(Config));
            Container.RegisterType<IControllerNameDetector, ControllerNameDetector>();
            Container.RegisterType<IControllerInformationParser, ControllerInformationParser>(new InjectionConstructor(Config));
            Container.RegisterType<IControllerNameParser, ControllerNameParser>();

        }

        public VersionDetectorConfigurationBase(IUnityContainer container, HttpConfiguration config)
        {
            Container = container;
            Config = config;
        }

        protected IUnityContainer Container { get; private set; }
    
        protected HttpConfiguration Config { get; private set; }
    }
}
