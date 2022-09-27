using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Context;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Repository
{
    public class CommunityRepository : BaseRepository<Community>, ICommunityRepository
    {
        public CommunityRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<Community>> GetAllCommunities()
        {
            var communities = await _context.Communities.Include(x => x.Address)
           .AsSplitQuery().ToListAsync();
            return communities;
        }

        public async Task<IList<Community>> GetApprovedCommunitiess()
        {
            var communities = await _context.Communities.Include(x => x.Address)
           .Where(e => e.IsApproved == true).ToListAsync();
            return communities;
        }

        public async Task<IList<Community>> GetCommunitiesByState(string state)
        {
            var communities = await _context.Communities.Include(x => x.Address)
             .Where(x => x.Address.State.ToUpper() == state.ToUpper())
             .ToListAsync();
            return communities;
        }

        public async Task<IList<Community>> GetCommunitiesByStreet(string street)
        {
            var communities = await _context.Communities.Include(x => x.Address)
            .Where(x => x.Address.Street.ToUpper() == street.ToUpper())
            .ToListAsync();
            return communities;
        }

        public async Task<IList<Community>> GetCommunitiessByLGA(string lga)
        {
            var communities = await _context.Communities.Include(x => x.Address)
           .Where(x => x.Address.LocalGovernment.ToUpper() == lga.ToUpper())
           .ToListAsync();
            return communities;
        }

        public async Task<Community> GetCommunityInfo(string id)
        {
            var community = await _context.Communities
           .Include(x => x.Address)
           .SingleOrDefaultAsync(e => e.Id == id);
            return community;
        }

        public async Task<IList<Community>> GetUnApprovedCommunities()
        {
            var communities = await _context.Communities.Include(x => x.Address)
           .Where(e => e.IsApproved == false).ToListAsync();
            return communities;
        }
    }
    
}
