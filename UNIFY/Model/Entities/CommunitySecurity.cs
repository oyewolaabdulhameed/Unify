using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class CommunitySecurity : AuditableEntity
    {
        public Community Community { get; set; }
        public string CommunityId { get; set; }
        public Security Security { get; set; }
        public string SecurityId { get; set; }
    }
}
