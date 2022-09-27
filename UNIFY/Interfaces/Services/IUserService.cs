using System.Threading.Tasks;
using static UNIFY.Dtos.UserDto;

namespace UNIFY.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserResponseModel> Login(UserRequestModel model);
    }
}
