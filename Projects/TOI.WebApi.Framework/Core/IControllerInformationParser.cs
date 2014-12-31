using System.Net.Http;

namespace TOI.WebApi.Framework.Core
{
    public interface IControllerInformationParser
    {
        Models.ControllerInformation GetControllerInformation(HttpRequestMessage requestMessage);
    }
}
