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

        public bool GameTime { get; set; }
        public Game Game { get; set; }

        //public int UserSelectListId { get; set; }
        //public SelectList UserSelectList { get; set; }

        public SelectList GameTypeSelectList { get; set; }

        public List<GameRowType> GameRowTypesForGivenGame { get; set; } 

        //public List<GameRow> GameRows { get; set; } 

        public GameRow GameRow { get; set; }
        public List<UserGameRow> UserGameRows { get; set; }
    }

    public class GameDetailsViewModel
    {
        public Game Game { get; set; }
    }
}