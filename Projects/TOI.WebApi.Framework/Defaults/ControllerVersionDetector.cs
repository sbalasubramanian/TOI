using TOI.WebApi.Framework.Core;

namespace TOI.WebApi.Framework.Defaults
{
    public sealed class ControllerVersionDetector : NamespaceControllerVersionDetector
    {
        private const string DefaultVersionPrefix = "_";

        protected override string VersionPrefix
        {
            get { return DefaultVersionPrefix; }
        }
    }
}