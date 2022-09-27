using System.Threading.Tasks;
using UNIFY.Dtos;

namespace UNIFY.Interfaces.Services
{
    public interface IAdminService
    {
        Task<BaseResponse> RegisterAdmin(AdminRequestModel model);

        Task<BaseResponse> UpdateAdmin(UpdateAdminRequestModel model, string id);

        Task<AdminResponseModel> GetAdminInfo(string id);

        Task<AdminsResponseModel> GetAllAdmins();

        Task<BaseResponse> ActivateAdmin(string id);

        Task<BaseResponse> DeActivateAdmin(string id);

        Task<AdminsResponseModel> GetAllActivatedAdmins();

        Task<AdminsResponseModel> GetAllDeactivatedAdmins();
    }
}
