using System;
using System.Collections.Generic;

namespace TOI.WebApi.Framework.Models
{
    public class ControllerInformationComparer : IEqualityComparer<ControllerInformation>
    {
        public bool Equals(ControllerInformation x, ControllerInformation y)
        {
            if (x == null || y == null)
            {
                return x == null && y == null;
            }

            return x.Equals(y);
        }

        public int GetHashCode(ControllerInformation obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }

            return obj.GetHashCode();
        }
    }
}
