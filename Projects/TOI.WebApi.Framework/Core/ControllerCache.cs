using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using TOI.WebApi.Framework.Utilities;
using ControllerInformation = TOI.WebApi.Framework.Models.ControllerInformation;

namespace TOI.WebApi.Framework.Core
{
    internal sealed class ControllerCache
    {
        public ICollection<Type> GetControllerTypes(ControllerInformation controllerId)
        {
            if (controllerId == null)
            {
                throw new ArgumentNullException("controllerId");
            }

            var matchingTypes = new HashSet<Type>();

            ILookup<string, Type> namespaceLookup;
            if (_cache.Value.TryGetValue(controllerId, out namespaceLookup))
            {
                foreach (var namespaceGroup in namespaceLookup)
                {
                    matchingTypes.UnionWith(namespaceGroup);
                }
            }

            return matchingTypes;
        }

        private Dictionary<ControllerInformation, ILookup<string, Type>> InitializeCache()
        {
            IAssembliesResolver assembliesResolver = _configuration.Services.GetAssembliesResolver();
            IHttpControllerTypeResolver controllersResolver = _configuration.Services.GetHttpControllerTypeResolver();
            var controllerInformationDetector = _configuration.DependencyResolver.Resolve<IControllerInformationDetector>();

            ICollection<Type> controllerTypes = controllersResolver.GetControllerTypes(assembliesResolver);
            IEnumerable<IGrouping<ControllerInformation, Type>> groupedByName =
                controllerTypes.Select(
                    controllerType => new { ClrType = controllerType, Id = controllerInformationDetector.GetControllerInformation(controllerType) })
                    .GroupBy(x => x.Id, x => x.ClrType);

            return groupedByName.ToDictionary(
                g => g.Key,
                g => g.ToLookup(t => t.Namespace ?? String.Empty, StringComparer.OrdinalIgnoreCase),
                ControllerInformation.Comparer);
        }

        internal Dictionary<ControllerInformation, ILookup<string, Type>> Cache
        {
            get { return _cache.Value; }
        }

        public ControllerCache(HttpConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentException("configuration");
            }

            _configuration = configuration;
            _cache = new Lazy<Dictionary<ControllerInformation, ILookup<string, Type>>>(InitializeCache);
        }

        private readonly Lazy<Dictionary<ControllerInformation, ILookup<string, Type>>> _cache;
        private readonly HttpConfiguration _configuration;
    }
}