using System.Collections.Generic;
using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class SecurityAgency : AuditableEntity
    {
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string Abbreviation { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public IList<Security> Securities { get; set; }
    }
}
