using System;

namespace TOI.WebApi.Framework.Models
{
    public abstract class ApiVersion : IEquatable<ApiVersion>, IComparable<ApiVersion>
    {
        public abstract bool Equals(ApiVersion other);
        

        public int CompareTo(ApiVersion other)
        {
            if (other == null)
            {
                return 1;
            }

            if (CanCompareTo(other))
            {
                return CompareToVersion(other);
            }

            return 0;
        }

        protected virtual bool CanCompareTo(ApiVersion other)
        {
            return other.GetType() == GetType();
        }

        protected virtual int CompareToVersion(ApiVersion other)
        {
            return 0;
        }

        public sealed override bool Equals(object obj)
        {
            var other = obj as ApiVersion;
            if (other != null)
            {
                return Equals(other);
            }

            return false;
        }

        public abstract override string ToString();

        public abstract override int GetHashCode();
    }
}
