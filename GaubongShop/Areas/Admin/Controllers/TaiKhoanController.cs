using GaubongShop.Models;
using GaubongShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GaubongShop.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: Admin/TaiKhoan
        private GauBongStoreEntities db = new GauBongStoreEntities();
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

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", " Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "TaiKhoan");

        }
    }
}