using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PersonGameRow
    {
        public int PersonGameRowId { get; set; }

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

        #region Foreign Keys

        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        
        public int GameRowId { get; set; }
        public virtual GameRow GameRow { get; set; }
        
        #endregion
    }
}
