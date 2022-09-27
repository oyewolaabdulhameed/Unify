using UNIFY.Context;
using UNIFY.Identity;
using UNIFY.Interfaces.Repository;

namespace UNIFY.Implementation.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }
    }
}
