//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Domain.Identity;

//namespace Domain.Models
//{
//    public class Person : BaseEntity
//    {
//        public int PersonId { get; set; }

//        //[Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
//        [MinLength(3, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
//        [MaxLength(12, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
//        [Display(Name = nameof(Resources.Domain.PersonName), ResourceType = typeof(Resources.Domain))]
//        public string PersonName { get; set; }

//        [Required(ErrorMessageResourceName = "FieldIsRequired", ErrorMessageResourceType = typeof(Resources.Common))]
//        [MinLength(3, ErrorMessageResourceName = "FieldMinLength", ErrorMessageResourceType = typeof(Resources.Common))]
//        [MaxLength(12, ErrorMessageResourceName = "FieldMaxLength", ErrorMessageResourceType = typeof(Resources.Common))]
//        [Display(Name = nameof(Resources.Domain.Nickname), ResourceType = typeof(Resources.Domain))]
//        public string Nickname { get; set; }


//        #region Foreign Keys

//        [ForeignKey(nameof(User))]
//        public int UserId { get; set; }
//        public virtual UserInt User { get; set; }         //Tied up with identity user.

//        public virtual ICollection<UserGameRow> PersonGameRows { get; set; }

//        public virtual ICollection<Game> Games { get; set; }

//        #endregion

//    }
//}
