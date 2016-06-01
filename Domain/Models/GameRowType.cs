using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GameRowType
    {
        public int GameRowTypeId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "RangeError")]
        [Display(Name = nameof(Resources.Domain.SortOrder), ResourceType = typeof(Resources.Domain))]
        public int SortOrder { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(20, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.GameRowDescription), ResourceType = typeof(Resources.Domain))]
        public string Description { get; set; }
        #region Foreign Keys

        //[ForeignKey(nameof(Description))]
        //public int DescriptionId { get; set; }
        //public virtual MultiLangString Description { get; set; }

        public int GameTypeId { get; set; } //ToDo, kas see on mõistlik.
        public virtual GameType GameType { get; set; }

        public virtual ICollection<GameRow> GameRows { get; set; } = new List<GameRow>();

        #endregion
    }
}
