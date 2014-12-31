using System.Web.Http;
using Microsoft.Practices.Unity;
using TOI.WebApi.Framework.Core;
using TOI.WebApi.Framework.Defaults;

namespace TOI.WebApi.Framework
{
    public class RouteVersionDetectorConfiguration : VersionDetectorConfigurationBase
    {
        public RouteVersionDetectorConfiguration(IUnityContainer container, HttpConfiguration config) : base (container, config)
        {
        }
        public override void Configure()
        {
            base.Configure();
            Container.RegisterType<IControllerVersionParser, ControllerVersionParser>();
        }
    }
}
