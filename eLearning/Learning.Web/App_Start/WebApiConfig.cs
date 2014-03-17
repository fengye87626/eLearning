using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Learning.Web.Filters;
using System.Web.Http.Dispatcher;

namespace Learning.Web
{
    public static class WebApiConfig
    {
        /*
        //default by VS 2013
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
         */

        public static void Register(HttpConfiguration config)
        {
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
            name: "Courses",
            routeTemplate: "api/courses/{id}",
            defaults: new { controller = "courses", id = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
                    name: "Enrollments",
                    routeTemplate: "api/courses/{courseId}/students/{userName}",
                    defaults: new { controller = "Enrollments", userName = RouteParameter.Optional }
                    );
                config.Routes.MapHttpRoute(
            name: "students",
            routeTemplate: "api/students/{userName}",
            defaults: new { controller = "students", userName = RouteParameter.Optional }
            );

          //config.Filters.Add(new ForceHttpsAttribute());

          config.Services.Replace(typeof(IHttpControllerSelector), new LearningControllerSelector((config)));
        }

    }
}
