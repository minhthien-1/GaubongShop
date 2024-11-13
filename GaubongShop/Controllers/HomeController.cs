using System.Web.UI;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GaubongShop.Models;
using GaubongShop.Models.ViewModel;
using PagedList;
using System.Linq;

namespace GaubongShop.Controllers
{
    public class HomeController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();
        public ActionResult Index()
        {
            return View();
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
            model.RealatedProducts = products.OrderBy(p => p.ProductID).Take(pageSize).ToList();//cần sửa lỗi 
            model.TopProducts = products.OrderByDescending(p=>p.OrderDetails.Count()).Take(3).ToPagedList(pageNumber, pageSize);
            if (quantity.HasValue)
            {
                model.quantity = quantity.Value;
            }
            return View(model);
        }
    }
}