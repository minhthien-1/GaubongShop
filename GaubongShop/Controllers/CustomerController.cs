using GaubongShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GaubongShop.Controllers
{
    public class CustomerController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();
        // GET: Customer
        [Authorize] // Chỉ cho phép người dùng đã đăng nhập truy cập
        public new ActionResult Profile()
        {
            // Lấy thông tin khách hàng dựa trên IDUser  của người dùng đã đăng nhập
            var username = User.Identity.Name;
            var user = db.Users.FirstOrDefault(u => u.Username == username);

            if (user == null)
            {
                return HttpNotFound();
            }

            var customer = db.Customers.FirstOrDefault(c => c.CustomerName == user.Username); // Hoặc c.Username == username

            if (customer == null)
            {
                return HttpNotFound();
            }

            return View(customer);
        }
    }
}