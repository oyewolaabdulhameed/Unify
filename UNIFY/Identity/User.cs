using System.Collections.Generic;
using UNIFY.Model.Base;
using UNIFY.Model.Entities;

namespace UNIFY.Identity
{
    public class User : AuditableEntity
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public Admin Admin { get; set; }
        public Member Member { get; set; }
        public Security Security{ get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }
}
