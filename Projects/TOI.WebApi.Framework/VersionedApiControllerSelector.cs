using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using TOI.WebApi.Framework.Core;
using TOI.WebApi.Framework.Utilities;
using ControllerInformation = TOI.WebApi.Framework.Models.ControllerInformation;

namespace TOI.WebApi.Framework
{
    public sealed class VersionedApiControllerSelector : IHttpControllerSelector
    {
        public HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            return OnSelectController(request);
        }

        public IDictionary<string, HttpControllerDescriptor> GetControllerMapping()
        {
            Dictionary<string, HttpControllerDescriptor> dict = new Dictionary<string, HttpControllerDescriptor>(_controllerInfoCache.Value.Select(x => x.Key.Name).Distinct().Count());

            foreach (var controllersByName in _controllerInfoCache.Value.GroupBy(x => x.Key.Name))
            {
                HttpControllerDescriptor current = null;
                string controllerKey = string.Empty;
                foreach (KeyValuePair<ControllerInformation, HttpControllerDescriptor> controllerWithVersion in controllersByName.OrderByDescending(x => x.Key.Version))
                {
                    if (current == null)
                    {
                        current = controllerWithVersion.Value;
                    }
                    controllerKey = string.Format("{0}-{1}", controllerWithVersion.Key.Version, controllerWithVersion.Key.Name);
                    //current.AddVersion(controllerWithVersion.Key.Version, controllerWithVersion.Value);
                }
                Debug.Assert(current != null, "This cannot be run as a grouping contains at least one controller...");
                dict[controllerKey] = current;
            }

            return dict;
        }

        private HttpControllerDescriptor OnSelectController(HttpRequestMessage request)
        {
            ControllerInformation controllerInfo = GetControllerIdentificationFromRequest(request);
            if (String.IsNullOrEmpty(controllerInfo.Name))
            {
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.NotFound));
            }

            HttpControllerDescriptor controllerDescriptor;
            if (_controllerInfoCache.Value.TryGetValue(controllerInfo, out controllerDescriptor))
            {
                return controllerDescriptor;
            }

            ICollection<Type> matchingTypes = _controllerCache.GetControllerTypes(controllerInfo);

            if (matchingTypes.Count == 0)
            {
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.NotFound,
                    "The API '" + controllerInfo + "' doesn't exist"));
            }

            throw new HttpResponseException(request.CreateResponse(HttpStatusCode.InternalServerError,
                CreateAmbiguousControllerExceptionMessage(request.GetRouteData().Route, controllerInfo.Name, matchingTypes)));
        }

        private ConcurrentDictionary<ControllerInformation, HttpControllerDescriptor> InitializeControllerInfoCache()
        {
            var result = new ConcurrentDictionary<ControllerInformation, HttpControllerDescriptor>(ControllerInformation.Comparer);
            var duplicateControllers = new HashSet<ControllerInformation>();
            Dictionary<ControllerInformation, ILookup<string, Type>> controllerGroups = _controllerCache.Cache;

            foreach (var controllerGroup in controllerGroups)
            {
                ControllerInformation controllerInfo = controllerGroup.Key;

                foreach (var controllerTypes in controllerGroup.Value)
                {
                    foreach (Type controllerType in controllerTypes)
                    {
                        if (result.Keys.Contains(controllerInfo))
                        {
                            duplicateControllers.Add(controllerInfo);
                            break;
                        }
                        result.TryAdd(controllerInfo, new HttpControllerDescriptor(_configuration, controllerInfo.Name, controllerType));
                    }
                }
            }

            foreach (ControllerInformation duplicateController in duplicateControllers)
            {
                HttpControllerDescriptor descriptor;
                result.TryRemove(duplicateController, out descriptor);
            }

            return result;
        }

        public VersionedApiControllerSelector(HttpConfiguration configuration)
        {
            _configuration = configuration;
            _controllerInformationParser = new Lazy<IControllerInformationParser>(() => _configuration.DependencyResolver.Resolve<IControllerInformationParser>());

            _controllerInfoCache = new Lazy<ConcurrentDictionary<ControllerInformation, HttpControllerDescriptor>>(InitializeControllerInfoCache);
            _controllerCache = new ControllerCache(_configuration);

        }

        private readonly HttpConfiguration _configuration;
        private readonly Lazy<IControllerInformationParser> _controllerInformationParser;
        private readonly Lazy<ConcurrentDictionary<ControllerInformation, HttpControllerDescriptor>> _controllerInfoCache;
        private readonly ControllerCache _controllerCache;

        private ControllerInformation GetControllerIdentificationFromRequest(HttpRequestMessage request)
        {
            IControllerInformationParser controllerIdentificationDetector = _controllerInformationParser.Value;
            ControllerInformation controllerInfo;

            try
            {
                controllerInfo = controllerIdentificationDetector.GetControllerInformation(request);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.BadRequest, ExceptionResources.CannotDetermineRequestVersion, ex));
            }

            if (controllerInfo == null)
            {
                throw new HttpResponseException(request.CreateResponse(HttpStatusCode.BadRequest, ExceptionResources.CannotDetermineRequestVersion));
            }

            return controllerInfo;
        }
        private static string CreateAmbiguousControllerExceptionMessage(IHttpRoute route, string controllerName, IEnumerable<Type> matchingTypes)
        {
            // Generate an exception containing all the controller types
            StringBuilder typeList = new StringBuilder();
            foreach (Type matchedType in matchingTypes)
            {
                typeList.AppendLine();
                typeList.Append(matchedType.FullName);
            }

            return String.Format(ExceptionResources.AmbigiousControllerRequest,
                                 controllerName,
                                 route.RouteTemplate,
                                 typeList);
        }
    }
}
