using System.Threading.Tasks;
using UNIFY.Dtos;
using UNIFY.Identity;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;

namespace UNIFY.Implementation.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<BaseResponse> Create(RoleDto.CreateRoleRequestModel model)
        {
            var roleExist = await _roleRepository.Get(a => a.RoleName == model.Name);
            if (roleExist != null) return new BaseResponse
            {
                Message = "Role already exist",
                Status = false,

            };

            var role = new Role
            {
                RoleName = model.Name,
                Description = model.Description,
            };

            await _roleRepository.Register(role);

            return new BaseResponse
            {
                Message = "Created Successfully",
                Status = true,
               
            };
        }

        //public Task<BaseResponse> Delete(string id)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task<RoleDto.RoleResponseodel> Get(string id)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task<RoleDto.RolesResponseodel> GetAll()
        //{
        //    throw new System.NotImplementedException();
        //}

        //public Task<BaseResponse> Update(string id, RoleDto.UpdateRoleRequestModel model)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
