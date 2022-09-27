using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Context;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Repository
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Member> GetMemberInfo(string id)
        {
            var member = await _context.Members
            .Include(x => x.User).Include(a => a.Address)
            .SingleOrDefaultAsync(x => x.User.Id == id && x.IsDeleted == false);
            return member;
        }
        public async Task<Member> GetMemberWithUser(string id)
        {
            var member = await _context.Members.Include(x => x.User).Include(a => a.Address).SingleOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted == false);
            return member;
        }
        public async Task<IList<Member>> GetAllMemberWithUser()
        {
            var members = await _context.Members.Include(x => x.User).Include(a => a.Address).Where(x => x.IsDeleted == false).ToListAsync();
            return members;
        }
        public async Task<IList<Member>> GetAllVerifiedMembers()
        {
            var Member = await _context.Members.Include(x => x.User).Include(a => a.Address).Where(x => x.IsDeleted == false && x.IsVerified == true).ToListAsync();
            return Member;
        }
        public async Task<IList<Member>> GetNotVerifiedMembers()
        {
            var members = await _context.Members.Include(x => x.User).Include(a => a.Address).Where(x => x.IsDeleted == false && x.IsVerified == false).ToListAsync();
            return members;
        }

        public async Task<IList<Community>> GetCommunitiesByMember(string id)
        {
            var communities = await _context.Communities.Where(x => x.CommunityId == id).Select(x => new Community
            {
                Id = x.Id,
                CommunityName = x.CommunityName,
                CommunityPhoneNumber = x.CommunityPhoneNumber,

            }).ToListAsync();
            return communities;
        }
    }
}
