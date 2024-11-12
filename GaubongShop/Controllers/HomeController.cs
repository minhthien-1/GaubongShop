using GaubongShop.Models.ViewModel;
using GaubongShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace GaubongShop.Controllers
{
    public class HomeController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();

        //get admin/product

        public ActionResult Index(string SearchTerm, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();
            //tìm kiếm sản phẩm dựa trên từ khoá
            if (string.IsNullOrEmpty(SearchTerm))
            {
                model.SearchTerm = SearchTerm;
                products = products.Where(p => p.ProductName.Contains(SearchTerm) ||
                                               p.ProductDecription.Contains(SearchTerm) ||
                                               p.Category.CategoryName.Contains(SearchTerm));
            }
            //đoạn code liên quan tới phân trang
            //lấy số trang hiện tại ( mặc định là trang 1 nếu không có giá trị)
            int PageNumber = page ?? 1;
            int PageSize = 6; //số sản phẩm mỗi trang

            ////lấy top 10 sản phẩm bán chạy nhất
            //model.FeaturedProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(10).ToList();

            ////lấy 20 sản phẩm bán ế nhất và phân trang
            //model.NewProducts = products.OrderBy(p => p.OrderDetails.Count()).Take(20).ToPagedList(PageNumber, PageSize);
            return View(model);
        }
        public ActionResult TrangChu()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}