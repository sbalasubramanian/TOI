using System;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Routing;
using TOI.WebApi.Framework.Models;

namespace TOI.WebApi.Framework.Core
{
    public abstract class RouteVersionParser : IControllerVersionParser
    {
        public ApiVersion GetVersion(HttpRequestMessage requestMessage)
        {
            var version = default(ApiVersion);
            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage");
            }

            IHttpRouteData routeData = requestMessage.GetRouteData();
            if (routeData != null)
            {
                version = GetVersionFromRouteData(routeData);
            }

            return version;
        }

        private ApiVersion GetVersionFromRouteData(IHttpRouteData routeData)
        {
            string versionString = GetStringRouteValue(routeData, RouteKey);

            if (versionString == null)
            {
                return null;
            }

            Version versionNumber = ParseVersionNumber(versionString);

            return new SemanticApiVersion(versionNumber);
        }

        private static Version ParseVersionNumber(string rawVersionNumber)
        {
            Version version = null;
            if (rawVersionNumber.IndexOf('.') == -1)
            {
                int singleVersionNumber;
                if (Int32.TryParse(rawVersionNumber, NumberStyles.None, CultureInfo.InvariantCulture, out singleVersionNumber))
                {
                    version = new Version(singleVersionNumber, 0);
                }
            }

            if (version == null && !Version.TryParse(rawVersionNumber, out version))
            {
                const string msg = "Cannot parse '{0}' as a version number";
                throw new Exception(String.Format(msg, rawVersionNumber));
            }

            return version;
        }

        private string GetStringRouteValue(IHttpRouteData routeData, string routeKey)
        {
            object controllerVersion;
            if (!routeData.Values.TryGetValue(routeKey, out controllerVersion))
            {
                const string msg = "Cannot retrieve the version number from the routing data. This probably means you haven't included a '{0}' key in your route configuration.";
                throw new InvalidOperationException(String.Format(msg, RouteKey));
            }

            if (controllerVersion == null)
            {
                return null;
            }

            string rawVersionNumber = Convert.ToString(controllerVersion, CultureInfo.InvariantCulture);
            return rawVersionNumber;
        }

        protected abstract string RouteKey { get; }
    }
}