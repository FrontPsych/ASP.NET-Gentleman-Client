using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Models;

namespace Web.ViewModels
{
    public class GameCreateViewModel
    {
        //[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        //[Range(1, 7, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "RangeError")]
        //[Display(Name = nameof(Resources.Common.NumberOfPlayers), ResourceType = typeof(Resources.Common))]
        //public int NumberOfPlayers { get; set; }
        public bool GameTime { get; set; }
        public Game Game { get; set; }



        public int UserSelectListId { get; set; }
        public SelectList UserSelectList { get; set; }

        public int[] UserMultiSelectListId { get; set; }
        public MultiSelectList UserMultiSelectList { get; set; }

        public SelectList GameTypeSelectList { get; set; }


        public List<UserGameRow> UserGameRows { get; set; }
        public List<GameRow> GameRows { get; set; } 
    }

    public class GameDetailsViewModel
    {
        public Game Game { get; set; }
    }
}