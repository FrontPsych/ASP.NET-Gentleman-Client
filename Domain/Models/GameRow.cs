using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GameRow : BaseEntity
    {
        public int GameRowId { get; set; }

        //ToDo: Mis väljad veel?

        #region Foreign Keys

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public int GameRowTypeId { get; set; }
        public virtual GameRowType GameRowType{ get; set; }

        public virtual ICollection<PersonGameRow> PersonGameRows { get; set; }

        #endregion
    }
}
