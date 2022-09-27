using System.Collections.Generic;
using System.Threading.Tasks;
using UNIFY.Dtos;
using static UNIFY.Dtos.RoleDto;

namespace UNIFY.Interfaces.Services
{
    public interface IRoleService
    {
        Task<BaseResponse> Create(CreateRoleRequestModel model);
        //Task<BaseResponse> Update(string id, UpdateRoleRequestModel model);
        //Task<RoleResponseodel> Get(string id);
        //Task<RolesResponseodel> GetAll();
        //Task<BaseResponse> Delete(string id);
    }
}
