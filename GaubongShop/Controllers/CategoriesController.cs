using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GaubongShop.Models;

namespace GaubongShop.Controllers
{
    public class CategoriesController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();

        // GET: Categories
       
        // Action PartialViewResult
        [ChildActionOnly]
        public PartialViewResult CategoryPartial()
        {
            var cateList = db.Categories.ToList();
            return PartialView(cateList);
        }
        
    }
}
