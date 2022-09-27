using System.Collections.Generic;

namespace UNIFY.Dtos
{
    public class UserDto
    {
        public class UserRequestModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class UserResponseModel : BaseResponse
        {
            public string Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Image { get; set; }
            public ICollection<RoleDto> Roles { get; set; } = new HashSet<RoleDto>();
        }
        public class LoginResponse
        {
            public UserResponseModel Data { get; set; }
            public string Token { get; set; }
        }
    }
}
