﻿using GaubongShop.Models.ViewModel;
using GaubongShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Web.Security;


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
              
                var user = new User
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
                return RedirectToAction("TrangChu", "Home");
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
                var user = db.Users.Find(model.Username, model.Password);
                if (user != null)
                {
                    //Lưu trạng thái vào session
                    Session["Username"] = user.Username;
                    Session["c"] = user.UserRole;

                    //Lưu thông tin xác thực người dùng vào cookie
                    FormsAuthentication.SetAuthCookie(user.Username, true);

                    return RedirectToAction("Index", "Home");
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
