using System.Collections.Generic;
using UNIFY.Identity;

namespace UNIFY.Interfaces.Repository
{
    public interface IRoleRepository : IRepository<Role>
    {
        IList<Role> GetRolesByUserId(string id);
    }
}
