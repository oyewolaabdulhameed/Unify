

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UNIFY.Context;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Repository
{
    public class SecurityRepository : BaseRepository<Security>, ISecurityRepository
    {
        public SecurityRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Security> GetSecurityInfo(string id)
        {
            var security = await _context.Securities
            .Include(x => x.User).Include(a => a.Address)
            .SingleOrDefaultAsync(x => x.User.Id == id && x.IsDeleted == false);
            return security;
        }
        public async Task<Security> GetSecurityWithUser(string id)
        {
            var security = await _context.Securities.Include(x => x.User).Include(a => a.Address).SingleOrDefaultAsync(x => x.Id.Equals(id) && x.IsDeleted == false);
            return security;
        }

        public async Task<IList<Security>> GetAllSecurityWithUser()
        {
            var security = await _context.Securities.Include(x => x.User).Include(a => a.Address).Where(x => x.IsDeleted == false).ToListAsync();
            return security;
        }

        public async Task<Security> Get(string id)
        {
            return await _context.Securities
               .Include(a => a.User)
               .Include(b => b.SecurityAgencies)
               .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Security>> GetSelected(List<string> id)
        {
            return await _context.Securities
                .Include(a => a.User)
                .Include(b => b.SecurityAgencies)
                .Where(a => id.Contains(a.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Security>> GetSelected(Expression<Func<Security, bool>> expression)
        {
            return await _context.Securities
               .Include(a => a.User)
               .Include(b => b.SecurityAgencies)
               .Where(expression)
               .ToListAsync();
        }

        async Task<IEnumerable<Security>> ISecurityRepository.GetAll()
        {
            return await _context.Securities
               .Include(a => a.User)
               .Include(b => b.SecurityAgencies)
               .ToListAsync();
        }
    }
}
