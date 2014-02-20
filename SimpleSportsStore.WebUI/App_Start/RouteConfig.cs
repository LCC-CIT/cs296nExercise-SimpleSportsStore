using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SimpleSportsStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /* Note: routes are processed in the order they are entered below */

            // ignore 
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            // Example URL: http://webhost/
            routes.MapRoute(null,
                "", 
                new {
                    controller = "Product", action = "List",
                    category = (string)null, page = 1
                }
            );

            /* Replaced by a new version in chapter 8
            // Example URL: http://webhost/Page2 
            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new { Controller = "Product", action = "List" }
                );
            */

            // Example URL: http://webhost/Page2
            // Parameter 2: string specifying the Url prameter name and, in curly braces,
            //              the name of a variable that will contain the url parameter value
            // Parameter 3: untyped object with default route values
            // Parameter 4: untyped object with url parameter values
            routes.MapRoute(null,
                "Page{page}", 
                new { controller = "Product", action = "List", category = (string)null },
                new { page = @"\d+" }
                );

            // Example URL: http://webhost/Soccer
            routes.MapRoute(null,
                "{category}",
                new { controller = "Product", action = "List", page = 1 }
            );

            // Example URL: http://webhost/Soccer/Page2
            routes.MapRoute(null,
                "{category}/Page{page}",
                new { controller = "Product", action = "List" },
                new { page = @"\d+" }
            );

            // Example URL: http://anything/Else
            routes.MapRoute(null, "{controller}/{action}");

            /* We are no longer using this default route
            routes.MapRoute(name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "List",
                id = UrlParameter.Optional}
                );
            */
        }
    }
}

/* Note: In the Default route, named arguments are being used in the call
 * to MapRoute. See Named and Optional Arguments (C# Programming Guide)
 * http://msdn.microsoft.com/en-us/library/dd264739.aspx
 */