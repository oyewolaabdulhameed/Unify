using System.Threading.Tasks;
using UNIFY.Dtos;
using static UNIFY.Dtos.SecurityDto;

namespace UNIFY.Interfaces.Services
{
    public interface ISecurityService
    {
        Task<BaseResponse> RegisterSecurity(SecurityRequestModel model);
        Task<BaseResponse> UpdateSecurity(UpdateSecurityRequestModel model, string id);
        Task<BaseResponse> DeleteSecurity(string id);
        Task<SecuritiesResponseModel> GetAllSecurities();
        Task<SecurityResponseModel> GetSecurityById(string id);
    }
}
