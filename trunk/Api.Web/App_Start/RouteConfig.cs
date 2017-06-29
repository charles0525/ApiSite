using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Api.Web.Models;
using Api.Web.Common;

namespace Api.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            try
            {
                ConstValues.ListRouteItems.ForEach(m =>
                {
                    routes.MapRoute(name: $"{m.ControllerName}_{m.ActionName}",
                        url: m.Url,
                        defaults: new
                        {
                            controller = "Home",
                            action = "ChildPage",
                            id = UrlParameter.Optional
                        });
                });
            }
            catch (Exception ex)
            {

            }

            routes.MapRoute(name: "error", url: "error", defaults: new { controller = "Home", action = "ErrorPage", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
           );
        }
    }
}
