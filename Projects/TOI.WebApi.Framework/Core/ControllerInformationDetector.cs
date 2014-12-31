using System;
using System.Web.Http;
using TOI.WebApi.Framework.Models;
using TOI.WebApi.Framework.Utilities;

namespace TOI.WebApi.Framework.Core
{
    public sealed class ControllerInformationDetector : IControllerInformationDetector
    {
        public Models.ControllerInformation GetControllerInformation(Type controllerType)
        {
            string name = GetControllerName(controllerType);
            ApiVersion version = GetControllerVersion(controllerType);

            if (name != null)
            {
                return new Models.ControllerInformation(name, version);
            }

            return null;
        }

        private ApiVersion GetControllerVersion(Type controllerType)
        {
            IControllerVersionDetector instance = _controllerVersionDetectorInstance.Value;

            return instance.GetVersion(controllerType);
        }

        private string GetControllerName(Type controllerType)
        {
            IControllerNameDetector instance = _controllerNameDetectorInstance.Value;

            return instance.GetControllerName(controllerType);
        }

        public ControllerInformationDetector(HttpConfiguration configuration)
        {
            _configuration = configuration;
            _controllerNameDetectorInstance = new Lazy<IControllerNameDetector>(() => _configuration.DependencyResolver.Resolve<IControllerNameDetector>());
            _controllerVersionDetectorInstance = new Lazy<IControllerVersionDetector>(() => _configuration.DependencyResolver.Resolve<IControllerVersionDetector>());
        }

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<IControllerNameDetector> _controllerNameDetectorInstance;
        private readonly Lazy<IControllerVersionDetector> _controllerVersionDetectorInstance;
    }
}
