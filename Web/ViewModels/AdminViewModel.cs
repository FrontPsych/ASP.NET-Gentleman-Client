using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Identity;
using Domain.Models;

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
}