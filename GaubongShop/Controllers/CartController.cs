using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GaubongShop.Models;
using GaubongShop.Models.ViewModel;

namespace GaubongShop.Controllers
{
    public class CartController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();
        //Hàm lấy dịch vụ giỏ hàng
        
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }
    }
}