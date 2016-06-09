using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain.Aggregates
{
    public class UserWithRole
    {
        public int UserIntId { get; set; }
        [Display(Name = nameof(Resources.Domain.Firstname), ResourceType = typeof(Resources.Domain))]
        public string FirstName { get; set; }
        [Display(Name = nameof(Resources.Domain.Lastname), ResourceType = typeof(Resources.Domain))]
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<RoleInt> UserRoles { get; set; } = new List<RoleInt>();
    }
}
