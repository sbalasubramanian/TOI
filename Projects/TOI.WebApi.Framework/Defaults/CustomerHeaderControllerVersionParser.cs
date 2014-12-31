using System;
using System.Linq;
using System.Net.Http;
using TOI.WebApi.Framework.Core;
using TOI.WebApi.Framework.Models;

namespace TOI.WebApi.Framework.Defaults 
{
    public class CustomerHeaderControllerVersionParser : IControllerVersionParser
    {

        public ApiVersion GetVersion(HttpRequestMessage requestMessage)
        {
            SemanticApiVersion version = null;
            if (requestMessage.Headers.Contains(VersionHeaderName))
            {
                var versionString = requestMessage.Headers.GetValues(VersionHeaderName).FirstOrDefault();
                Version ver = new Version(versionString);
                version = new SemanticApiVersion(ver);
            }

            return version;
        }

        public virtual string VersionHeaderName 
        {
            get { return CustomVersionHeaderName; }
        }

        private const string CustomVersionHeaderName = "api-version";
    }
}