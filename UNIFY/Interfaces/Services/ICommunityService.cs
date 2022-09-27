using System.Threading.Tasks;
using UNIFY.Dtos;

namespace UNIFY.Interfaces.Services
{
    public interface ICommunityService
    {
        Task<CommunityResponseModel> RegisterCommunity(CommunityRequestModel model);
        Task<BaseResponse> UpdateCommunity(UpdateCommunityRequestModel model, string Id);
        Task<CommunityResponseModel> GetCommunityInfo(string id);
        Task<CommunityResponseModel> GetCommunityById(string id);
        Task<CommunitiesResponseModel> GetAllCommunities();
        Task<BaseResponse> ApproveCommunity(string id);
        Task<CommunitiesResponseModel> GetApprovedCommunities();
        Task<CommunitiesResponseModel> GetUnApprovedCommunities();
        Task<BaseResponse> DisApproveCommunity(string id);
        Task<CommunitiesResponseModel> GetCommunitiesByState(string state);
        Task<CommunitiesResponseModel> GetCommunitiesByStreet(string street);
        Task<CommunitiesResponseModel> GetCommunitiesByLGA(string Lga);

    }
}
