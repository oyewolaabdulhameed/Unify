using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Context;
using UNIFY.Interfaces.Repository;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Repository
{
    public class AdminRepository: BaseRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<Admin> GetAdminInfo(string id)
        {
            var admin = await _context.Admins
            .Include(x => x.User)
            .SingleOrDefaultAsync(x => x.User.Id == id && x.IsDeleted == false);
            return admin;
        }
        public async Task<IList<Admin>> GetAllAdminWithUser()
        {
            var admin = await _context.Admins.Include(x => x.User).Where(x => x.IsDeleted == false).ToListAsync();
            return admin;
        }
        public async Task<IList<Admin>> GetAllActivatedAdmin()
        {
            var admin = await _context.Admins
                .Include(f => f.User)
                .Where(x => x.IsDeleted == false).ToListAsync();
            return admin;
        }
        public async Task<IList<Admin>> GetAllDeactivatedAdmin()
        {
            var admin = await _context.Admins
                .Include(f => f.User)
                .Where(x => x.IsDeleted == true).ToListAsync();
            return admin;
        }
    }
}
