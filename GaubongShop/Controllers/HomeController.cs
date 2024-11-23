using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GaubongShop.Models;
using GaubongShop.Models.ViewModel;
using PagedList;

namespace GaubongShop.Controllers
{
    public class HomeController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TrangChu(int? category, int? page, string SearchString, double min = double.MinValue, double max = double.MaxValue)
        {
            var products = db.Products.Include(p => p.Category);
            // Tìm kiếm chuỗi truy vấn theo category
            if (category == null)
            {
                products = db.Products.OrderByDescending(x => x.ProductName);
            }
            else
            {
                products = db.Products.OrderByDescending(x => x.CategoryID).Where(x => x.CategoryID == category);
            }
            // Tìm kiếm chuỗi truy vấn theo NamePro (SearchString)
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = db.Products.OrderByDescending(x => x.CategoryID).Where(s => s.ProductName.Contains(SearchString.Trim()));
            }
            // Tìm kiếm chuỗi truy vấn theo đơn giá
            if (min >= 0 && max > 0)
            {
                products = db.Products.OrderByDescending(x => x.ProductPrice).Where(p => (double)p.ProductPrice >= min && (double)p.ProductPrice <= max);
            }
            // Khai báo mỗi trang 4 sản phẩm
            int pageSize = 4;
            // Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // Nếu page = null thì đặt lại page là 1.
            if (page == null) page = 1;

            // Trả về các product được phân trang theo kích thước và số trang.
            return View(products.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult giaohangtannoi()
        {
            return View();
        }
        public ActionResult bocquagiare()
        {
            return View();
        }
        public ActionResult tangthiepmienphi()
        {
            return View();
        }
        public ActionResult giatgaubong()
        {
            return View();
        }
        public ActionResult nennhogau()
        {
            return View();
        }
        public ActionResult batdaumuasam()
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
        public ActionResult ProductDetails(int?id, int?quantity, int? page)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if(pro == null )
            {
                return HttpNotFound();
            }
            //lấy tất cả các sản phẩm cùng danh mục
            var products = db.Products.Where(p=>p.CategoryID==pro.CategoryID &&p.ProductID!=pro.ProductID).AsQueryable();
            ProductDetailVM model = new ProductDetailVM();
            //Đoạn code liên quan tới phân trang 
            //Lấy số trang hiện tại (mặc định là trang 1 nếu trang không có giá trị)
            int pageNumber = page ?? 1;
            int pageSize = model.PageSize; //số sản phẩm mỗi trang
            model.product = pro;
            model.RealatedProducts = products.OrderBy(p => p.ProductID).Take(3).ToList();
            model.TopProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(3).ToPagedList(pageNumber, pageSize);
            if (quantity.HasValue)
            {
                model.quantity = quantity.Value;
            }
            return View(model);
        }
    }
}