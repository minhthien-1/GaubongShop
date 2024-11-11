using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaubongShop.Models.ViewModel
{
    public class ProductSearchVM
    {
        public string SearchTerm { get; set; }
        public List<Product> Products { get; set; }
        public string SortOrder { get; set; }
    }
}