using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sniper.Umbraco.SirTrevor
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
               name: "SirTrevorImageUpload",
               url: "App_Plugins/SirTrevor/Upload/Image",
               defaults: new { controller = "SirTrevor", action = "UploadImage", id = UrlParameter.Optional }
           );
        }
    }
}