using System.Collections.Generic;
using System.Threading.Tasks;
using UNIFY.Model.Entities;

namespace UNIFY.Interfaces.Repository
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member> GetMemberWithUser(string id);

        Task<Member> GetMemberInfo(string id);

        Task<IList<Member>> GetAllMemberWithUser();

        Task<IList<Community>> GetCommunitiesByMember(string id);

        Task<IList<Member>> GetAllVerifiedMembers();

        Task<IList<Member>> GetNotVerifiedMembers();
    }
}
