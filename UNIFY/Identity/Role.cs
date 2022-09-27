using System.Collections.Generic;
using UNIFY.Model.Base;

namespace UNIFY.Identity
{
    public class Role : AuditableEntity
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
