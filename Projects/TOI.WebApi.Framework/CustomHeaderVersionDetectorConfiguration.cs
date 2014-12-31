using System.Web.Http;
using Microsoft.Practices.Unity;
using TOI.WebApi.Framework.Core;
using TOI.WebApi.Framework.Defaults;

namespace TOI.WebApi.Framework
{
    public class CustomHeaderVersionDetectorConfiguration : VersionDetectorConfigurationBase
    {
        public override void Configure()
        {
            base.Configure();
            Container.RegisterType<IControllerVersionParser, CustomerHeaderControllerVersionParser>();
            
        }
        public CustomHeaderVersionDetectorConfiguration(IUnityContainer container, HttpConfiguration config) : base(container, config)
        {
            
        }
    }
}
