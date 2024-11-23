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

        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)
                return View("ShowCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);
        }
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Cart model)
        {
            // Kiểm tra giỏ hàng trong session
            var cart = Session["Cart"] as Cart;
            if (cart == null || cart.Items == null || !cart.Items.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng trống. Vui lòng thêm sản phẩm trước khi thanh toán.");
                return View(model);
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

            if (ModelState.IsValid)
            {
                // Tạo đơn hàng mới
                var order = new Order
                {
                    CustomerID = customer.CustomerID,
                    OrderDate = DateTime.Now,
                    AddressDelivery = customer.CustomerAddress,
                    TotalAmount = cart.Items.Sum(item => item.TotalPrice) // Tổng tiền
                };

                db.Orders.Add(order);
                db.SaveChanges();

                // Lưu chi tiết từng sản phẩm vào OrderDetails
                foreach (var item in cart.Items)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderID = order.OrderID,
                        ProductID = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };

                    db.OrderDetails.Add(orderDetail);
                }

                db.SaveChanges();

                // Xóa giỏ hàng sau khi đặt hàng thành công
                Session["Cart"] = null;

                // Chuyển hướng tới trang xác nhận đơn hàng
                return RedirectToAction("OrderConfirmation", "Order", new { id = order.OrderID });
            }

            // Nếu không hợp lệ, trả về trang thanh toán
            return View(model);
        }

        //Hàm lấy dịch vụ giỏ hàng
        private CartService GetCartService()
        {
            return new CartService(Session);
        }
        //Hiển thị giỏ hàng không gom nhóm theo danh mục
        public ActionResult Index()
        {
            var cart = GetCartService().GetCart();
            return View(cart);
        }
        //Thêm sản phẩm vào giỏ
        public ActionResult AddToCart(int id, int quanity = 1)
        {
            var product = db.Products.Find(id);
            if (product!=null)
            {
                var cartService = GetCartService();
                cartService.GetCart().AddItem(product.ProductID, product.ProductImage,
                    product.ProductName, product.ProductPrice, quanity, product.Category.CategoryName);
            }
            return RedirectToAction("Index");
        }
        //Xóa sản phẩm khỏi giỏ
        public ActionResult RemoveFromCart(int id)
        {
            var cartService = GetCartService();
            cartService.GetCart().RemoveItem(id);
            return RedirectToAction("Index");
        }
        //Làm trống giỏ hàng
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int id, int quantity)
        {
            var carService = GetCartService();
            carService.GetCart().UpdateQuantity(id, quantity);
            return RedirectToAction("Index");
        }
        
    }
}