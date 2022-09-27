using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UNIFY.Interfaces.Services;
using static UNIFY.Dtos.RoleDto;

namespace UNIFY.Controllers
{
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RoleController(IRoleService roleService, IWebHostEnvironment webHostEnvironment)
        {
            _roleService = roleService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("RegisterRole")]
        public async Task<IActionResult> RegisterRole([FromForm] CreateRoleRequestModel model)
        {
            var registerRole = await _roleService.Create(model);
            if (registerRole.Status == false)
            {
                return BadRequest(registerRole);
            }
            return Ok(registerRole);
        }
    }
}
