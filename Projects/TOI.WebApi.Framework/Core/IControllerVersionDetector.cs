using System;
using TOI.WebApi.Framework.Models;

namespace TOI.WebApi.Framework.Core
{
    public interface IControllerVersionDetector
    {

        ApiVersion GetVersion(Type controllerType);
    }
}