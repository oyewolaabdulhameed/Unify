using System.Collections.Generic;
using UNIFY.Identity;
using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class Security:AuditableEntity
    {
        public string Image { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string SecurityId { get; set; }
        public string CommunityId { get; set; }
        public string SecurityAgencyId { get; set; }
        public SecurityAgency SecurityAgencies { get; set; }
        public string SecurityFirstName { get; set; }
        public string SecurityLastName { get; set; }
        public string SecurityPhoneNumber { get; set; }
        public string SecurityEmail { get; set; }
        public string Password { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public string SecurityOrganization { get; set; }
        public ICollection<CommunitySecurity> CommunitySecurities { get; set; } = new HashSet<CommunitySecurity>();
    }
}
