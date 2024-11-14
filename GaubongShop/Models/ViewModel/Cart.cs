using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace GaubongShop.Models.ViewModel
{
    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();

        public IEnumerable<CartItem> Items => items;
        
        //Thêm sản phẩm vào giỏ
        public void AddItem(int productId, string productImage, string productName,decimal unitPrice,int quantity,string category)
        {
            var existingItem = items.FirstOrDefault(i => i.ProductId==productId); 
        }
    }
}