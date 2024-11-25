using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GaubongShop.Controllers
{
    public class CheckoutVMController : Controller
    {
        // GET: CheckoutVM
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Checkout()
        {
            return View();
        }
    }
}