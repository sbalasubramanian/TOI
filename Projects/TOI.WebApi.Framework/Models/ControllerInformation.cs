using System;
using System.Collections.Generic;

namespace TOI.WebApi.Framework.Models
{
    public class ControllerInformation : IEquatable<ControllerInformation>
    {
        public ApiVersion Version { get; private set; }
        public string  Name { get; private set; }

        public bool Equals(ControllerInformation other)
        {
            bool isEqual = other != null;
            isEqual = isEqual && String.Equals(other.Name, Name, StringComparison.OrdinalIgnoreCase);

            if (Version != null)
            {
                isEqual = isEqual && Version.Equals(other.Version);
            }

            return isEqual;
        }

        public sealed override bool Equals(object obj)
        {
            var other = obj as ControllerInformation;
            if (other != null)
            {
                return Equals(other);
            }

            return false;
        }

        public sealed override int GetHashCode()
        {
            unchecked
            {
                int result = Name.GetHashCode();
                result = Version.GetHashCode() >> 3 ^ result;

                return result;
            }
        }
        
        public ControllerInformation(string name, ApiVersion version)
        {
            Name = name;
            Version = version;
        }

        public static readonly IEqualityComparer<ControllerInformation> Comparer = new ControllerInformationComparer();
    }
}
