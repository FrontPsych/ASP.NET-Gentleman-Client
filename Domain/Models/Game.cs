using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain.Models
{
    public class Game : BaseEntity
    {

        public Game()
        {
                StartedAt = DateTime.Now;
        }

        public int GameId { get; set; }

        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        [MinLength(3, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [MaxLength(12, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
        [Display(Name = nameof(Resources.Domain.GameName), ResourceType = typeof(Resources.Domain))]
        public string GameName { get; set; }

        [Display(Name = nameof(Resources.Domain.StartedAt), ResourceType = typeof(Resources.Domain))]
        public DateTime StartedAt { get; set; }

        [Display(Name = nameof(Resources.Domain.StoppedAt), ResourceType = typeof(Resources.Domain))]
        public DateTime? StoppedAt { get; set; }

        #region Foreign Keys

        [Required(ErrorMessageResourceName = "SelectionIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
        public int GameTypeId { get; set; }
        public virtual GameType GameType { get; set; }

        public int UserIntId { get; set; }
        public virtual UserInt UserInt { get; set; }

        public virtual ICollection<GameRow> GameRows { get; set; }

        #endregion
    }
}
