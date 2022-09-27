using UNIFY.Identity;
using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class Address : AuditableEntity
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string LocalGovernment { get; set; }
        public string Street { get; set; }
        public string AddressDescription { get; set; }
        public Member Member { get; set; }
        public Security Security { get; set; }
        public SecurityAgency SecurityAgency { get; set; }
          
    }
}
