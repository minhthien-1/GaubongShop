using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaubongShop.Models.ViewModel
{
    public class OrderStatisticsVM
    {
        public int OrderCount { get; set; }
        public decimal TotalAmount { get; set; }
    }
}