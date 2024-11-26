using GaubongShop.Models;
using GaubongShop.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace GaubongShop.Controllers
{
    public class OrderController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();
        //[HttpGet]
        [Authorize]
        public ActionResult Checkout()
        {
            //Kiểm tra giỏ hàng trong session 
            //nếu giỏ hàng rỗng hoặc không có sản phẩm thì chuyển hướng về trang chủ
            var cart = Session["Cart"] as Cart;
            if (cart.Items == null || !cart.Items.Any())
            {
                return RedirectToAction("TrangChu", "Home");
            }
            //Xác thực người dùng đã đăng nhập chưa, nếu chưa thì hướng tới đăng nhập
            var user = db.Users.SingleOrDefault(u=>u.Username == User.Identity.Name);
            if (user == null)
            {
               return RedirectToAction("Login", "Account");
            }
            //Lấy thông tin khách hàng từ csdl, nếu chưa có thì chuyển hướng tới trang Đăng nhập
            //Nếu có rồi thì lấy địa chỉ khách hàng và gắn vào shippingAddress của checkoutVM
            var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            if (customer == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new CheckoutVM
            {
                //Tạo dữ liệu hiển thị cho checkoutVM
                CartItems = cart.Items.ToList(),
                TotalAmount = cart.Items.Sum(item => item.TotalPrice),//Tổng giá trị của các mặt hàng trong giỏ
                OrderDate = DateTime.Now,//Mặt định lấy bằng thời điểm đặt hàng
                ShippingAddress = customer.CustomerAddress, //Lấy địa chỉ mặc định từ Customer
                CustomerID = customer.CustomerID, //Lấy mã khách hàng từ Customer
                Username = customer.Username //Lấy tên đăng nhập từ Cus

            };
            return View(model);

            //// Kiểm tra giỏ hàng trong session
            //var cart = Session["Cart"] as Cart;
            //if (cart == null || cart.Items == null || !cart.Items.Any())
            //{
            //    TempData["ErrorMessage"] = "Giỏ hàng trống. Vui lòng thêm sản phẩm trước khi thanh toán.";
            //    return RedirectToAction("Index", "Cart"); // Chuyển hướng tới trang giỏ hàng
            //}

            //// Xác thực người dùng đã đăng nhập
            //var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            //if (user == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            //// Lấy thông tin khách hàng từ CSDL
            //var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            //if (customer == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            //// Chuẩn bị model cho trang thanh toán
            //var model = new CheckoutVM
            //{
            //    CartItems = (List<CartItem>)cart.Items,
            //    CustomerID = customer.CustomerID,
            //    ShippingAddress = customer.CustomerAddress,
            //    Email = customer.CustomerEmail,
            //    TotalAmount = cart.Items.Sum(item => item.TotalPrice)
            //};

            //return View(model);
        }
        //Post: Order/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var cart = Session["Cart"] as Cart;
                //Nếu giỏ hàng rỗng sẽ điều hướng tới trang Home
                if (cart.Items == null || !cart.Items.Any()) 
                {
                    return RedirectToAction("TrangChu", "Home");
                }
                // Xác thực người dùng đã đăng nhập
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Lấy thông tin khách hàng từ CSDL
                var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
                if (customer == null)
                {
                    return RedirectToAction("Login", "Account");
                }
                //Nếu người dùng chọn thanh toán bằng Paypal, sẽ điều hướng tới trang PaymentWithPayal
                if (model.PaymentMethod == "Payal")
                {
                    return RedirectToAction("PaymentWithPaypal", "PayPal", model);
                }
                //Thiết lập PaymentStatus dựa trên PaymentMethod
                string paymentStatus = "Chưa thanh toán";
                switch (model.PaymentMethod)
                {
                    case "Tiền mặt": paymentStatus = "Thanh toán tiền mặt"; break;
                    case "Paypal": paymentStatus = "Thanh toán paypal"; break;
                    case "Mua trước trả sau": paymentStatus = "Trả góp"; break;
                    default: paymentStatus = "Chưa thanh toán"; break;
                }
                var order = new Order
                {
                    CustomerID = customer.CustomerID,
                    OrderDate = DateTime.Now, // Ghi nhận thời gian đặt hàng là hiện tại
                    TotalAmount = cart.Items.Sum(item => item.TotalPrice), // Tổng tiền của đơn hàng
                    PaymentStatus = paymentStatus,
                    PaymentMethod = model.PaymentMethod,
                    ShippingMethod = model.ShippingMethod,
                    ShippingAddress = model.ShippingAddress,
                    DeliveryMethod = "Giao hàng nhanh", // Có thể là giá trị mặc định hoặc từ input
                    OrderDetails = cart.Items.Select(item => new OrderDetail
                    {
                        ProductID = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.Quantity * item.UnitPrice
                    }).ToList()

                };
                //Lưu đơn hàng vào CSDL
                db.Orders.Add(order);
                db.SaveChanges();
                //Xóa giỏ hàng sau khi đặt hàng thành công
                Session["Cart"] = null;
                //Điều hướng tới trang xác nhận đơn hàng
                return RedirectToAction("OrderConfirmation", "OrderConfirmation", new { id = order.OrderID });

            }
            return View(model);


            //if (ModelState.IsValid)
            //{
            //    var cart = Session["Cart"] as List<CartItem>;
            //    //Nếu giỏ hàng rỗng sẽ điều hướng tới trang Home
            //    if (cart == null || !cart.Any())
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //    // Xác thực người dùng đã đăng nhập
            //    var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
            //    if (user == null)
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }

            //    // Lấy thông tin khách hàng từ CSDL
            //    var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
            //    if (customer == null)
            //    {
            //        return RedirectToAction("Login", "Account");
            //    }
            //    //Nếu người dùng chọn thanh toán bằng Paypal, sẽ điều hướng tới trang PaymentWithPayal
            //    if (model.PaymentMethod == "Payal")
            //    {
            //        return RedirectToAction("PaymentWithPaypal", "PayPal", model);
            //    }
            //    //Thiết lập PaymentStatus dựa trên PaymentMethod
            //    string paymentStatus = "Chưa thanh toán";
            //    switch (model.PaymentMethod)
            //    {
            //        case "Tiền mặt": paymentStatus = "Thanh toán tiền mặt"; break;
            //        case "Paypal": paymentStatus = "Thanh toán paypal"; break;
            //        case "Mua trước trả sau": paymentStatus = "Trả góp"; break;
            //        default: paymentStatus = "Chưa thanh toán"; break;
            //    }
            //    //Tạo đơn hàng và chi tiết đơn hàng liên quan
            //    var order = new Order
            //    {
            //        CustomerID = customer.CustomerID,
            //        OrderDate = model.OrderDate,
            //        TotalAmount = model.TotalAmount,
            //        PaymentStatus = paymentStatus,
            //        PaymentMethod = model.PaymentMethod,
            //        ShippingMethod = model.ShippingMethod,
            //        ShippingAddress = model.ShippingAddress,
            //        OrderDetails = cart.Select(item => new OrderDetail
            //        {
            //            ProductID = item.ProductId,
            //            Quantity = item.Quantity,
            //            UnitPrice = item.UnitPrice,
            //            TotalPrice = item.TotalPrice
            //        }).ToList()

            //    };
            //    //Lưu đơn hàng vào CSDL
            //    db.Orders.Add(order);
            //    db.SaveChanges();
            //    //Xóa giỏ hàng sau khi đặt hàng thành công
            //    Session["Cart"] = null;
            //    //Điều hướng tới trang xác nhận đơn hàng
            //    return RedirectToAction("OrderConfirmation", new { id = order.OrderID });

            }
            //return View(model);
        }
    }
