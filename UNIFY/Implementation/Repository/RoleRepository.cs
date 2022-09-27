using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using UNIFY.Context;
using UNIFY.Identity;
using UNIFY.Interfaces.Repository;

namespace UNIFY.Implementation.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IList<Role> GetRolesByUserId(string id)
        {
            var roles = _context.UserRoles.Include(r => r.Role).Where(x => x.UserId == id).Select(r => new Role
            {
                RoleName = r.Role.RoleName,
                Description = r.Role.Description
            }).ToList();
            return roles;
        }
    }
}
