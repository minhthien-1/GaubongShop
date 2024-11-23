using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaubongShop.Models.ViewModel
{
    public class OrderConfirmationVM
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string ShippingAddress { get; set; }
        public string PaymentStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemVM> Items { get; set; }
    }
}