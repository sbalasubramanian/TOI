using System;
using System.Net.Http;
using System.Web.Http;
using TOI.WebApi.Framework.Models;
using TOI.WebApi.Framework.Utilities;

namespace TOI.WebApi.Framework.Core
{
    public sealed class ControllerInformationParser : IControllerInformationParser
    {
        public ControllerInformation GetControllerInformation(HttpRequestMessage requestMessage)
        {
            string name = GetControllerName(requestMessage);
            ApiVersion version = GetControllerVersion(requestMessage);

            if (name != null)
            {
                return new ControllerInformation(name, version);
            }

            return null;
        }

        private ApiVersion GetControllerVersion(HttpRequestMessage requestMessage)
        {
            IControllerVersionParser instance = _controllerVersionDetectorInstance.Value;

            return instance.GetVersion(requestMessage);
        }

        private string GetControllerName(HttpRequestMessage requestMessage)
        {
            IControllerNameParser instance = _controllerNameDetectorInstance.Value;

            return instance.GetControllerName(requestMessage);
        }

        public ControllerInformationParser(HttpConfiguration configuration)
        {
            _configuration = configuration;
            _controllerNameDetectorInstance = new Lazy<IControllerNameParser>(() => _configuration.DependencyResolver.Resolve<IControllerNameParser>());
            _controllerVersionDetectorInstance = new Lazy<IControllerVersionParser>(() => _configuration.DependencyResolver.Resolve<IControllerVersionParser>());
        }

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<IControllerNameParser> _controllerNameDetectorInstance;
        private readonly Lazy<IControllerVersionParser> _controllerVersionDetectorInstance;
    }
}
