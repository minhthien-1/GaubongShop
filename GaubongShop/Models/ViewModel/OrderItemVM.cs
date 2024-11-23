using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaubongShop.Models.ViewModel
{
    public class OrderItemVM
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}