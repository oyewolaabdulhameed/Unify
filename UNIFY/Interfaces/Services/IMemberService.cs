using System.Threading.Tasks;
using UNIFY.Dtos;

namespace UNIFY.Interfaces.Services
{
    public interface IMemberService
    {
        Task<BaseResponse> RegisterMember(MemberRequestModel model);

        Task<BaseResponse> UpdateMember(UpdateMemberRequestModel model, string Id);

        Task<MembersResponseModel> GetMemberInfo(string id);

        Task<MemberResponseModel> GetAllMembers();

        Task<CommunitiesResponseModel> GetCommunitiesByMemberId(string id);

        Task<BaseResponse> VerifyMember(string id);

        Task<MembersResponseModel> GetMemberById(string id);

        Task<MemberResponseModel> GetAllVerifiedMembers();

        Task<MemberResponseModel> GetNotVerifiedMembers();
    }
}
