using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace GaubongShop.Models.ViewModel
{
    public class CartItem
    {
        public int ProductId { get; set; }  
        public string ProductName { get;set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get;set; }
        public string ProductImage { get;set; }
        //Tổng giá cho mọi sản phẩm
        public decimal TotalPrice => Quantity*UnitPrice;
    }
}