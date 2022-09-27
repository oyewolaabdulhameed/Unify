using System.Collections.Generic;
using System.Threading.Tasks;
using UNIFY.Model.Entities;

namespace UNIFY.Interfaces.Repository
{
    public interface ICommunityRepository : IRepository<Community>
    {
        Task<Community> GetCommunityInfo(string id);
        Task<IList<Community>> GetUnApprovedCommunities();
        Task<IList<Community>> GetApprovedCommunitiess();
        Task<IList<Community>> GetCommunitiesByState(string state);
        Task<IList<Community>> GetCommunitiesByStreet(string street);
        Task<IList<Community>> GetCommunitiessByLGA(string lga);
        Task<IList<Community>> GetAllCommunities();
    }
}
