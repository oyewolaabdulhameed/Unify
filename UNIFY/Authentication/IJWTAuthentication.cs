using static UNIFY.Dtos.UserDto;

namespace UNIFY.Authentication
{
    public interface IJWTAuthentication
    {
        string GenerateToken(UserResponseModel model);
    }
}
