using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GaubongShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Route cho trang thông tin khách hàng
            routes.MapRoute(
                name: "CustomerProfile",
                url: "Customer/Profile",
                defaults: new { controller = "Customer", action = "Profile" },
                namespaces: new[] { "GaubongShop.Controllers" } // Specifies main namespace
            );
            // route cho productlist
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Products", action = "ProductList", id = UrlParameter.Optional },
                namespaces: new[] { "GaubongShop.Controllers" }
                );// Specifies main namespace
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "GaubongShop.Controllers" } // Specifies main namespace


            );
        }
    }
}
