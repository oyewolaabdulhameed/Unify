using System.Collections.Generic;
using UNIFY.Identity;
using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class Member : AuditableEntity
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Image { get; set; }
        public string PhoneNumber { get; set; }
        public User User { get; set; }
        public bool IsVerified { get; set; }
        public string CommunityId { get; set; }
        public Community Community { get; set; } 
        public string MessageId { get; set; }
        public Message Messages { get; set; }
        public string? Skills { get; set; }
        public string Portfolio { get; set; }
        public string AddressId { get; set; }
        public Address Address { get; set; }
    }
}
