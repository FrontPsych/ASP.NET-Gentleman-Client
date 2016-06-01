using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class GameType
    {
        public int GameTypeId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MinLength(3, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(40, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.GameTypeName), ResourceType = typeof(Resources.Domain))]
        public string Name { get; set; }

        #region Foreign Keys

        [ForeignKey(nameof(Description))]
        public int? DescriptionId { get; set; }
        public virtual MultiLangString Description { get; set; }

        public virtual ICollection<Game> Games { get; set; } = new List<Game>();

        public virtual ICollection<GameRowType> GameRowTypes { get; set; } = new List<GameRowType>();

        #endregion
    }
}
