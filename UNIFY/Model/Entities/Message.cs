using System.Collections.Generic;
using UNIFY.Model.Base;
using UNIFY.Model.Enums;

namespace UNIFY.Model.Entities
{
    public class Message : AuditableEntity
    {
       public string MessageId { get; set; }
       public MessageType MessageType { get;set; }
       public Member Member { get; set; }
       public List<Comment> Comments { get; set; }

    }
}
