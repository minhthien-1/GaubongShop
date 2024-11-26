using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GaubongShop.Models.ViewModel
{
    public class CheckoutVM
    {
        public List<CartItem> CartItems { get; set; } /*= new List<CartItem>();*/
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        public string ShippingAddress { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức giao hàng")]
        public string ShippingMethod { get; set; }
        public int CustomerID { get; set; }

        [Display(Name = "Ngày đặt hàng")]
        
        public System.DateTime OrderDate { get; set; }

        [Display(Name = "Tổng giá trị")]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Trạng thái thanh toán")]
        public string PaymentStatus { get; set; }


        public string Username { get; set; }
        public int PhoneNumber { get; set; }

        [Display(Name = "Số điện thoại")]

       
        public string Email { get; set; }
        [Display(Name = "Địa chỉ giao hàng")]
        //Các thuộc tính khác của đơn hàng
        public List<OrderDetail> OrderDetails { get; set; }



    }
}