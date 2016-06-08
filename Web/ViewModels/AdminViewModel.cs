using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Identity;
using Domain.Models;
using PagedList;

namespace Web.ViewModels
{
    public class AdminIndexViewModel
    {
        public UserInt LoggedInUser { get; set; }
    }

    public class AdminAboutViewModel
    {
        public Article AboutArticle { get; set; }
    }

    public class AdminUserListViewModel
    {
        public IPagedList<UserInt> Users  { get; set; }
        public List<RoleInt> Roles { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
        public string SortProperty { get; set; }
        public string Filter { get; set; }
        public bool FilterByDTBoolean { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FilterFromDT { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? FilterToDT { get; set; }

    }
}