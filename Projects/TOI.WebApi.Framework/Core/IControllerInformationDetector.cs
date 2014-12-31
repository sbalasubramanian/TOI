using System;

namespace TOI.WebApi.Framework.Core
{
    public interface IControllerInformationDetector
    {

        Models.ControllerInformation GetControllerInformation(Type controllerType);
    }
}