using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GaubongShop.Models.ViewModel
{
    public class UserASearchVm
    {
        public string SearchTerm { get; set; }
        public List<User>Users { get; set; }
    }
}