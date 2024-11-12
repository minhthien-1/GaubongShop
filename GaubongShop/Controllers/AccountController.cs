using GaubongShop.Models.ViewModel;
using GaubongShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Security;
using System.Web.UI.WebControls;


namespace GaubongShop.Controllers
{
    public class AccountController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {

            if (ModelState.IsValid)
            {
                 Models.User existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại!");
                    return View(model);
                }
                var user = new Models.User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "M"
                };
                db.Users.Add(user);
                db.SaveChanges();
                var customer = new Customer
                {
                    CustomerEmail = model.CustomerEmail,
                    CustomerName = model.CustomerName,
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,
                };
                db.Customers.Add(customer);

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        //GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        //POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username
                                                    && u.Password == model.Password);
                if (user != null)
                {
                    //Lưu trạng thái vào session
                    Session["Username"] = user.Username;
                    

                    //Lưu thông tin xác thực người dùng vào cookie
                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    return RedirectToAction("TrangChu", "Home");
                }
                else
                {
                    ModelState.AddModelError("", " Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }
        //GET: Account/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");

        }
    }
}
