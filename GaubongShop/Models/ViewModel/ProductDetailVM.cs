using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using PagedList.Mvc;
namespace GaubongShop.Models.ViewModel
{
    public class ProductDetailVM
    {
        public Product product { get; set; }
        public int quantity { get; set; } = 1;
        //tinh gia tri tam thoi 
        public decimal estimatedValue => quantity * product.ProductPrice;
        //Các thuộc tính hỗ trợ phân trang 
        public int PageNumber { get; set; } //trang hiện tại
        public int PageSize { get; set; } //số sản phẩm mỗi trang 
        //Danh sách 3 sản phẩm cùng danh mục
        public List<Product> RealatedProducts { get; set;}
        //Danh sách 3 sản phẩm nổi bật 
        public PagedList.IPagedList<Product> TopProducts { get; set; }  
    }
}