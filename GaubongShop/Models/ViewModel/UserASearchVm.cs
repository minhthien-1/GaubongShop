using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;

namespace GaubongShop.Models.ViewModel
{
    public class UserASearchVm
    {
        public string SearchTerm { get; set; }
        //các thuộc tính hỗ trợ phân trang 
        public int PageNumber { get; set; }
        public int PageSize { get; set; } = 3;
        //Danh sách sản phẩm đã phân trang
        public PagedList.IPagedList<User>Users { get; set; }
    }
}