using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UNIFY.Authentication;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;
using static UNIFY.Dtos.UserDto;

namespace UNIFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTAuthentication _auth;
        private readonly ICommunityRepository _communityService;
        private readonly IMemberService _memberService;
        public LogInController(IUserService userService,IMemberService memberService, ICommunityRepository communityService, IJWTAuthentication auth)
        {
            _userService = userService;
            _auth = auth;
            _communityService = communityService;
            _memberService = memberService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserRequestModel model)
        {
            var login = await _userService.Login(model);
            if (!login.Status)
            {
                return BadRequest(login);
            }
            var token = _auth.GenerateToken(login);
            var response = new LoginResponse
            {
                Data = login,
                Token = token
            };
            return Ok(response);
        }
    }
}
