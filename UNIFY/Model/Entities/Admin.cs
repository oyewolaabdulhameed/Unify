using UNIFY.Identity;
using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class Admin : AuditableEntity
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
        public string PhoneNumber { get; set; }


    }
}
