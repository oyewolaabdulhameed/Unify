using System.Collections.Generic;
using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class Community : AuditableEntity
    {
        public string CommunityDocument { get; set; }
        public string CommunityId { get; set; }
        public string CommunityName { get; set; }
        public string CommunityPhoneNumber { get; set; }
        public bool IsApproved { get; set; }
        public List<Member> Members { get; set; }
        public string MemberId { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
        public string MarketId { get; set; }
        public MarketPlace MarketPlace { get; set; }
        public ICollection<CommunitySecurity> CommunitySecurities { get; set; } = new HashSet<CommunitySecurity>();
    }
}
