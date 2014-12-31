using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using TOI.WebApi.Framework.Models;

namespace TOI.WebApi.Framework.Core
{
    public abstract class AcceptHeaderControllerVersionParser : IControllerVersionParser
    {
        public ApiVersion GetVersion(HttpRequestMessage requestMessage)
        {
            if (requestMessage == null)
            {
                throw new ArgumentNullException("requestMessage");
            }

            var acceptHeader = requestMessage.Headers.Accept;

            return GetVersionFromHeader(acceptHeader);
        }

        private ApiVersion GetVersionFromHeader(IEnumerable<MediaTypeWithQualityHeaderValue> acceptHeader)
        {
            return acceptHeader.OrderByDescending(x => x.Quality).Select(GetVersionFromHeader).FirstOrDefault(version => version != null);
        }

        protected abstract ApiVersion GetVersionFromHeader(MediaTypeWithQualityHeaderValue headerValue);

        public virtual string HeaderParameterName 
        {
            get { return CustomHeaderParameterName; }
        }

        private const string CustomHeaderParameterName = "api-version";
    }
}
