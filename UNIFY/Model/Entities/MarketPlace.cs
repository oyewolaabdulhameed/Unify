using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class MarketPlace : AuditableEntity
    {
        public string MarketId { get; set; }
        public string MarketName { get; set; }
        public string CommunityId { get; set; }
        public Community Community { get; set; }
    }
}
