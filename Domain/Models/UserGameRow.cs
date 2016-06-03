using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain.Models
{
    public class UserGameRow
    {
        public int UserGameRowId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "RangeError")]
        [Display(Name = nameof(Resources.Domain.StartBet), ResourceType = typeof(Resources.Domain))]
        public int StartBet { get; set; }

        [Range(0, int.MaxValue, ErrorMessageResourceType = typeof(Resources.Common), ErrorMessageResourceName = "RangeError")]
        [Display(Name = nameof(Resources.Domain.EndBet), ResourceType = typeof(Resources.Domain))]
        public int? EndBet { get; set; }

        [MaxLength(250, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.PersonGameRowComment), ResourceType = typeof(Resources.Domain))]
        public string Comment { get; set; }

        [NotMapped]
        public string Index { get; set; } //needed for EditorForMany


        #region Foreign Keys

        public int UserIntId { get; set; }
        public virtual UserInt UserInt { get; set; }
        
        public int GameRowId { get; set; }
        public virtual GameRow GameRow { get; set; }
        
        #endregion
    }
}
