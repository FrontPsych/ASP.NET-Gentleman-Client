using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Identity;

namespace Domain.Aggregates
{
    public class UserWithRole
    {
        public int UserIntId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<RoleInt> UserRoles { get; set; } = new List<RoleInt>();
    }
}
