using System.Net.Http;
using TOI.WebApi.Framework.Models;

namespace TOI.WebApi.Framework.Core
{
    public interface IControllerVersionParser
    {
        ApiVersion GetVersion(HttpRequestMessage requestMessage);
    }
}
