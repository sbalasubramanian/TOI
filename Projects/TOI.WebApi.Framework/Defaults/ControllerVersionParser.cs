using TOI.WebApi.Framework.Core;

namespace TOI.WebApi.Framework.Defaults 
{
    public sealed class ControllerVersionParser : RouteVersionParser
    {
        private const string DefaultRouteKey = "version";

        protected override string RouteKey {
            get { return DefaultRouteKey; }
        }
    }
}