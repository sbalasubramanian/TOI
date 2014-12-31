using System;

namespace TOI.WebApi.Framework.Core
{
    public interface IControllerNameDetector
    {

        string GetControllerName(Type controllerType);
    }
}