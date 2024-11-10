using System.Web.Mvc;

namespace GaubongShop.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                 defaults: new { action = "Index", id = UrlParameter.Optional },
            namespaces: new[] { "GaubongShop.Areas.Admin.Controllers" }  // Chỉ định namespace cho Admin
            );
        }
    }
}