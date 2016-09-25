﻿using PlaySimple.Common;
using PlaySimple.Filters;
using System;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace PlaySimple
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            GlobalConfiguration.Configuration.Filters.Add(new AllowCORSFilterAttribute());

            // Adds authorization / authentication for all requests to controllers
            GlobalConfiguration.Configuration.Filters.Add(new GlobalAuthorizationFilter());

	        // Adds a filter that validate that the data sent from the client is valid.
	        GlobalConfiguration.Configuration.Filters.Add(new ValidateModelAttribute());

            // formats errors for client
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            // logs errors to db
            config.Services.Add(typeof(IExceptionLogger), new GlobalExceptionLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
             new PlaySimpleDateConverter());

            config.ParameterBindingRules
              .Add(typeof(DateTime?), des => new DateTimeParameterBinding(des));
        }
    }
}
