using System;

namespace TOI.WebApi.Framework.Models {

    public class SemanticApiVersion : ApiVersion
    {
        public SemanticApiVersion(Version version)
        {
            Version = version;
        }

        public Version Version { get; private set; }

        public override bool Equals(ApiVersion other) {
            return other is SemanticApiVersion && ((SemanticApiVersion)other).Version == Version;
        }

        public override string ToString() {
            return Version.ToString();
        }

        protected override int CompareToVersion(ApiVersion other) {
            return Version.CompareTo(((SemanticApiVersion) other).Version);
        }

        public override int GetHashCode()
        {
            return Version.GetHashCode();
        }
    }
}