using System;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Routing;

namespace TOI.WebApi.Framework.Core
{

    public abstract class RouteControllerNameParser : IControllerNameParser
    {

        public string GetControllerName(HttpRequestMessage requestMessage)
        {
            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage");
            }

            IHttpRouteData routeData = requestMessage.GetRouteData();
            if (routeData == null)
            {
                return default(String);
            }

            return GetControllerNameFromRouteData(routeData);
        }

        private string GetControllerNameFromRouteData(IHttpRouteData routeData)
        {

            object controllerName;
            routeData.Values.TryGetValue(RouteKey, out controllerName);

            return Convert.ToString(controllerName, CultureInfo.InvariantCulture);
        }
        protected abstract string RouteKey { get; }
    }
}