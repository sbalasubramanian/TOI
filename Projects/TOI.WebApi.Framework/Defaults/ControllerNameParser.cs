using TOI.WebApi.Framework.Core;

namespace TOI.WebApi.Framework.Defaults 
{
    public sealed class ControllerNameParser : RouteControllerNameParser 
    {
        protected override string RouteKey 
        {
            get { return DefaultRouteKey; }
        }

        private const string DefaultRouteKey = "controller";
    }
}