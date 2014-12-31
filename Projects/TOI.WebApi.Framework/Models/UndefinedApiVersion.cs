using System;

namespace TOI.WebApi.Framework.Models {

    public sealed class UndefinedApiVersion : ApiVersion {
        private UndefinedApiVersion() {}

        public override bool Equals(ApiVersion other) {
            return ReferenceEquals(other, this);
        }

        public override string ToString() {
            return String.Empty;
        }

        public override int GetHashCode() {
            return 0;
        }

        public static readonly ApiVersion Instance = new UndefinedApiVersion();
    }
}