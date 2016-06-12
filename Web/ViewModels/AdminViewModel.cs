using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain.Aggregates;
using Domain.Identity;
using Domain.Models;
using PagedList;

namespace Web.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<GameStatistic> Games { get; set; } 
        public List<Friend> Friends { get; set; } 
        public List<Friend> FriendRequests { get; set; } 
        public string SearchTxt { get; set; }
    }

    public class AdminAboutViewModel
    {
        public Article AboutArticle { get; set; }
    }

    public class AdminUserListViewModel
    {
        public IPagedList<UserWithRole> Users  { get; set; }
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