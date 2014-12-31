using System;
using System.Collections.Generic;
using System.Globalization;
using TOI.WebApi.Framework.Models;

namespace TOI.WebApi.Framework.Core
{
    public abstract class NamespaceControllerVersionDetector : IControllerVersionDetector
    {
        protected abstract string VersionPrefix { get; }

        public ApiVersion GetVersion(Type controllerType)
        {
            string namespaceName = controllerType.Namespace;

            if (String.IsNullOrEmpty(namespaceName))
            {
                return UndefinedApiVersion.Instance;
            }

            return GetVersionForNamespace(controllerType, namespaceName.Split('.'));
        }

        protected virtual ApiVersion GetVersionForNamespace(Type controllerType, string[] namespaceParts)
        {
            string prefix = VersionPrefix;

            foreach (string namespacePart in GetPossibleVersionNamespaceParts(namespaceParts, prefix))
            {
                ApiVersion apiVersion = GetApiVersion(namespacePart);

                if (apiVersion != null)
                {
                    return apiVersion;
                }
            }

            return null;
        }

        protected virtual ApiVersion GetApiVersion(string namespacePartWithoutPrefix)
        {
            string apiVersionAsParsable = namespacePartWithoutPrefix.Replace('_', '.');

            Version version = null;
            if (apiVersionAsParsable.IndexOf('.') == -1)
            {
                int singleVersionNumber;
                if (Int32.TryParse(apiVersionAsParsable, NumberStyles.None, CultureInfo.InvariantCulture, out singleVersionNumber))
                {
                    version = new Version(singleVersionNumber, 0);
                }
            }

            if (version == null && !Version.TryParse(apiVersionAsParsable, out version))
            {
                return UndefinedApiVersion.Instance;
            }

            return new SemanticApiVersion(version);
        }

        protected virtual List<String> GetPossibleVersionNamespaceParts(string[] namespaceParts, string prefix)
        {
            var parts = new List<string>(1);

            for (int index = 0; index <= namespaceParts.Length - 1; index++)
            {
                string part = namespaceParts[index];
                if (part.StartsWith(prefix, StringComparison.OrdinalIgnoreCase) && part.Length > prefix.Length)
                {
                    parts.Add(part.Substring(prefix.Length));
                }
            }

            return parts;
        }
    }
}