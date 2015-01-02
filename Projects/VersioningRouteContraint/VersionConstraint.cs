using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;

namespace VersioningRouteContraint
{
    internal class VersionConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (routeDirection == HttpRouteDirection.UriResolution)
            {
                var version = GetRequestVersion(request);
                return version == AllowedVersion;
            }
            return true;
        }

        private double GetRequestVersion(HttpRequestMessage request)
        {
            IEnumerable<string> headerValues;
            double version = DefaultVersion;
            if (request.Headers.TryGetValues(VersionHeaderName, out headerValues))
            {
                var versionAsString = headerValues.FirstOrDefault();
                if (!string.IsNullOrEmpty(versionAsString) && double.TryParse(versionAsString, out version))
                {
                    return version;
                }
            }

            return version;
        }
        public double AllowedVersion
        {
            get;
            private set;
        }
        public VersionConstraint(int allowedVersion)
        {
            AllowedVersion = allowedVersion;
        }

        public const string VersionHeaderName = "api-version";
        private const double DefaultVersion = 1;
    }
}