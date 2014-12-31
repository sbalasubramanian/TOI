using System;
using System.Web.Http.Dispatcher;

namespace TOI.WebApi.Framework.Core
{
    public sealed class ControllerNameDetector : IControllerNameDetector
    {
        public string GetControllerName(Type controllerType)
        {
            string suffix = DefaultHttpControllerSelector.ControllerSuffix;

            string typeName = controllerType.Name;
            int suffixIndex = typeName.LastIndexOf(suffix, StringComparison.OrdinalIgnoreCase);

            if (suffixIndex < 1)
            {
                return null;
            }

            return typeName.Substring(0, suffixIndex);
        }
    }
}