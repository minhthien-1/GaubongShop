using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GaubongShop.Models;
using GaubongShop.Models.ViewModel;

namespace GaubongShop.Controllers
{
    public class OderConfirmationController : Controller
    {
        private GauBongStoreEntities db = new GauBongStoreEntities();
        // GET: OderConfirmation
       public ActionResult OrderConfirmation(int id)
        {
            // Lấy thông tin đơn hàng từ cơ sở dữ liệu dựa trên OrderID
            var order = db.Orders
                .Include("OrderDetails.Product") // Nạp thông tin chi tiết đơn hàng và sản phẩm
                .SingleOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return HttpNotFound("Không tìm thấy đơn hàng.");
            }

            // Tạo ViewModel cho trang xác nhận đơn hàng
            var model = new OrderConfirmationVM
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                CustomerName = order.Customer?.CustomerName, // Lấy tên khách hàng
                ShippingAddress = order.AddressDelivery, // Địa chỉ giao hàng
                PaymentStatus = order.PaymentStatus,
                TotalAmount = order.TotalAmount,
                Items = order.OrderDetails.Select(od => new OrderItemVM
                {
                    ProductName = od.Product.ProductName,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice,
                    TotalPrice = od.Quantity * od.UnitPrice
                }).ToList()
            };

            return View(model);
        }
    }
}