using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UNIFY.Dtos;
using UNIFY.Interfaces.Services;

namespace UNIFY.Controllers
{
    [Route("api/[controller]")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MemberController(IMemberService memberService, IWebHostEnvironment webHostEnvironment)
        {
            _memberService = memberService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost("RegisterMember")]
        public async Task<IActionResult> RegisterMember([FromForm] MemberRequestModel model) 
        {
            var registermember = await _memberService.RegisterMember(model);
            if (registermember.Status == false)
            {
                return BadRequest(registermember);
            }
            return Ok(registermember);
        }
        [HttpPut("UpdateMember/{id}")]
        public async Task<IActionResult> UpdateMember([FromForm] UpdateMemberRequestModel model, [FromRoute] string id) 
        {
            var member = await _memberService.UpdateMember(model, id);
            if (!member.Status)
            {
                return BadRequest(member);
            }
            return Ok(member);
        }
        [HttpGet("GetMemberById/{id}")]
        public async Task<IActionResult> GetMemberById([FromRoute] string id)
        {
            var member = await _memberService.GetMemberInfo(id);
            if (member.Status)
            {
                return Ok(member);
            }
            return BadRequest(member);
        }
        [HttpGet("GetAllMembers")]
        public async Task<IActionResult> GetAllMembers()
        {
            var member = await _memberService.GetAllMembers();
            return Ok(member);
        }

        [HttpGet("GetCommunitiesByMemberId/{id}")]
        public async Task<IActionResult> GetCommunitiesByMemberId([FromRoute] string id)
        {
            var members = await _memberService.GetCommunitiesByMemberId(id);
            if (members.Status)
            {
                return Ok(members);
            }
            return BadRequest(members);
        }
        [HttpPut("VerifyMember/{id}")]
        public async Task<IActionResult> VerifyMember([FromRoute] string id)
        {
            var member = await _memberService.VerifyMember(id);
            if (member != null)
            {
                return Ok(member);
            }
            return BadRequest(member);
        }
        [HttpGet("GetAllVerifiedMembers")]
        public async Task<IActionResult> GetAllVerifiedMembers()
        {
            var members = await _memberService.GetAllVerifiedMembers();
            return Ok(members);
        }

        [HttpGet("GetNotVerifiedMembers")]
        public async Task<IActionResult> GetNotVerifiedMembers()
        {
            var member = await _memberService.GetNotVerifiedMembers();
            return Ok(member);
        }
    }
}
