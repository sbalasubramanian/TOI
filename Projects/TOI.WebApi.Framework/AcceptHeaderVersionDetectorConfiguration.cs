using System.Web.Http;
using Microsoft.Practices.Unity;
using TOI.WebApi.Framework.Core;
using TOI.WebApi.Framework.Defaults;

namespace TOI.WebApi.Framework
{
    public class AcceptHeaderVersionDetectorConfiguration : VersionDetectorConfigurationBase
    {
        public override void Configure()
        {
            base.Configure();
            Container.RegisterType<IControllerVersionParser, AcceptHeaderApiVersionParser>();
            
        }
        public AcceptHeaderVersionDetectorConfiguration(IUnityContainer container, HttpConfiguration config) : base(container, config)
        {
            
        }
    }
}
