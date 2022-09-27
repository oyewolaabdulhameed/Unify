using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UNIFY.Dtos;
using UNIFY.Interfaces.Services;

namespace UNIFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ICommunityService _communityService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CommunityController(ICommunityService communityService, IWebHostEnvironment webHostEnvironment)
        {
            _communityService = communityService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("RegisterCommunity")]
        public async Task<IActionResult> RegisterCommunity([FromForm] CommunityRequestModel model)
        {
            var registerCommunity = await _communityService.RegisterCommunity(model);
            if (registerCommunity.Status == false)
            {
                return BadRequest(registerCommunity);
            }
            return Ok(registerCommunity);
        }

        [HttpPut("UpdateCommunity/{id}")]
        public async Task<IActionResult> UpdateCommunity([FromForm] UpdateCommunityRequestModel model, [FromRoute] string id)
        {
            var community = await _communityService.UpdateCommunity(model, id);
            if (!community.Status)
            {
                return BadRequest(community);
            }
            return Ok(community);
        }

        [HttpGet("GetCommunityById/{id}")]
        public async Task<IActionResult> GetCommunityById([FromRoute] string id)
        {
            var community = await _communityService.GetCommunityInfo(id);
            if (community.Status)
            {
                return Ok(community);
            }
            return BadRequest(community);
        }

        [HttpGet("GetAllCommunities")]
        public async Task<IActionResult> GetAllCommunities()
        {
            var communities = await _communityService.GetAllCommunities();
            return Ok(communities);
        }

        [HttpGet("GetCommunitiesByStreet")]
        public async Task<IActionResult> GetCommunitiesByStreet(string state)
        {
            var communities = await _communityService.GetCommunitiesByStreet(state);
            if (communities.Status)
            {
                return Ok(communities);
            }
            return BadRequest(communities);
        }

        [HttpGet("GetCommunitiesByState")]
        public async Task<IActionResult> GetCommunitiesByState(string state)
        {
            var communities = await _communityService.GetCommunitiesByState(state);
            if (communities.Status)
            {
                return Ok(communities);
            }
            return BadRequest(communities);
        }


        [HttpGet("GetCommunitiesByLGA")]
        public async Task<IActionResult> GetApartmentsByLGA(string LGA)
        {
            var communities = await _communityService.GetCommunitiesByLGA(LGA);
            if (communities.Status)
            {
                return Ok(communities);
            }
            return BadRequest(communities);
        }

        [HttpPost("DisApproveCommunity")]
        public async Task<IActionResult> DisApproveCommunity(string id)
        {
            var community = await _communityService.DisApproveCommunity(id);
            if (community.Status)
            {
                return Ok(community);
            }
            return BadRequest(community);
        }

        [HttpGet("GetUnApprovedCommunities")]
        public async Task<IActionResult> GetUnApprovedCommunities()
        {
            var communities = await _communityService.GetUnApprovedCommunities();
            if (communities.Status)
            {
                return Ok(communities);
            }
            return BadRequest(communities);
        }

        [HttpPut("ApproveCommunity/{id}")]
        public async Task<IActionResult> ApproveCommunity([FromRoute] string id)
        {
            var community = await _communityService.ApproveCommunity(id);
            if (community.Status)
            {
                return Ok(community);
            }
            return BadRequest(community);
        }

        [HttpGet("GetApprovedCommunities")]
        public async Task<IActionResult> GetApprovedCommunities()
        {
            var communities = await _communityService.GetApprovedCommunities();
            if (communities.Status)
            {
                return Ok(communities);
            }
            return BadRequest(communities);
        }
    }
}
