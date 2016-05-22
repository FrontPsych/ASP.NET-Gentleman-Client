using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GameRow : BaseEntity
    {
        public GameRow(Game game, GameRowType x)
        {
            Game = game;
            GameRowType = x;
        }

        public int GameRowId { get; set; }

        public string Index { get; set; } //needed for EditorForMany

        //ToDo: Mis väljad veel?

        #region Foreign Keys

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int GameRowTypeId { get; set; }
        public virtual GameRowType GameRowType{ get; set; }

        public virtual ICollection<UserGameRow> UserGameRows { get; set; }

        #endregion
    }
}
