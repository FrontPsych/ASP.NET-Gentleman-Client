using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Models;

namespace Web.ViewModels
{
    public class AllFriendsUserVariaViewModel
    {
        public List<Friend> AllFriends { get; set; } 
    }

    public class StatisticsUserVariaViewModel
    {
        public int AllGameTypeSelectListId { get; set; }
        public SelectList AllGameTypeSelectList { get; set; }
    }
}