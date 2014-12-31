using System.Net.Http;

namespace TOI.WebApi.Framework.Core
{
    public interface IControllerNameParser
    {
        string GetControllerName(HttpRequestMessage requestMessage);
    }
}
