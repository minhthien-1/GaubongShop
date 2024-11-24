using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace GaubongShop.Models.ViewModel
{
    public class SearchCustomerVM
    {
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 10;
        public PagedList.IPagedList<Customer> Customers { get; set; }
        //public List<Customer> Customers { get; set; }
    }
}