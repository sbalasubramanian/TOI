using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using TOI.WebApi.Framework;

namespace WebApiVersioningTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //// Version in Route
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/v{version}/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //new RouteVersionDetectorConfiguration(new UnityContainer(), config).Configure();

            // Version in Accept header.
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            new AcceptHeaderVersionDetectorConfiguration(new UnityContainer(), config).Configure();

            //// Version in Custom header.
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            //new AcceptHeaderVersionDetectorConfiguration(new UnityContainer(), config).Configure();

        }
    }
}
