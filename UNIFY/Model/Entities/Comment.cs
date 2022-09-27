using UNIFY.Model.Base;

namespace UNIFY.Model.Entities
{
    public class Comment:AuditableEntity
    {
        public string CommentId { get; set; }
        public string CommentText { get; set; }
        public Message Message { get; set; }
        public string MessageId { get; set; }
    }
}
