using System.Collections.Generic;
using System.Threading.Tasks;
using UNIFY.Model.Entities;

namespace UNIFY.Interfaces.Repository
{
    public interface IAdminRepository : IRepository<Admin>
    {
        Task<Admin> GetAdminInfo(string id);

        Task<IList<Admin>> GetAllAdminWithUser();

        Task<IList<Admin>> GetAllActivatedAdmin();

        Task<IList<Admin>> GetAllDeactivatedAdmin();
    }
}
