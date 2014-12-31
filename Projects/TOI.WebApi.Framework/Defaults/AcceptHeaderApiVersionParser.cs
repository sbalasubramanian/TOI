using System;
using System.Linq;
using System.Net.Http.Headers;
using TOI.WebApi.Framework.Core;
using TOI.WebApi.Framework.Models;

namespace TOI.WebApi.Framework.Defaults
{
    public sealed class AcceptHeaderApiVersionParser : AcceptHeaderControllerVersionParser
    {
        protected override ApiVersion GetVersionFromHeader(MediaTypeWithQualityHeaderValue headerValue)
        {
            SemanticApiVersion requestVersion = null;

            if (String.Equals(headerValue.MediaType, AcceptMediaType, StringComparison.InvariantCultureIgnoreCase))
            {
                var versionParameter = headerValue.Parameters.FirstOrDefault(parameter => string.Equals(parameter.Name, HeaderParameterName, StringComparison.InvariantCultureIgnoreCase));

                if (versionParameter != null && !String.IsNullOrEmpty(versionParameter.Value))
                {
                    requestVersion = new SemanticApiVersion(new Version(versionParameter.Value));
                }
            }
            return requestVersion;
        }

        private const string AcceptMediaType = "application/json";

    }
}
