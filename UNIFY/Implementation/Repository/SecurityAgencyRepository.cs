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
    public class SecurityAgencyRepository : BaseRepository<SecurityAgency>, ISecurityAgencyRepository
    {
        public SecurityAgencyRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<SecurityAgency> Get(string id)
        {
            return await _context.SecurityAgencies
                 .Include(b => b.Securities).ThenInclude(c => c.User)
                 .FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted == false);
        }

        public async Task<IEnumerable<SecurityAgency>> GetSelected(List<string> id)
        {
            return await _context.SecurityAgencies
               .Include(b => b.Securities)
               .Where(c => id.Contains(c.Id) && c.IsDeleted == false)
               .ToListAsync();
        }

        public async Task<IEnumerable<SecurityAgency>> GetSelected(Expression<Func<SecurityAgency, bool>> expression)
        {
            return await _context.SecurityAgencies
                .Include(b => b.Securities)
                .Where(expression)
                .ToListAsync();
        }

         async Task<IEnumerable<SecurityAgency>> ISecurityAgencyRepository.GetAll()
        {
            return await _context.SecurityAgencies
               .Include(b => b.Securities).ThenInclude(c => c.User)
               .Where(c => c.IsDeleted == false)
               .ToListAsync();
        }
    }
}
