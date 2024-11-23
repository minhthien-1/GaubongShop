using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaubongShop.Areas.Admin.Views.Customers
{
    public class CustomerSearchVM
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;
        public CustomerSearchVM() { }
    }
}