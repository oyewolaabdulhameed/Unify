using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace UNIFY.Dtos
{
    public class CommunityDto
    {
        public string CommunityName { get; set; }
        public string CommunityEmail { get; set; }
        public string CommunityPhoneNumber { get; set; }
        public bool? IsApproved { get; set; }
        public string Id { get; set; }
        public string? MemberId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string LGA { get; set; }
        public string AddressDescription { get; set; }
    }
    public class CommunityRequestModel
    {
        public string CommunityName { get; set; }
        public string CommunityEmail { get; set; }
        public string CommunityPhoneNumber { get; set; }
        public IFormFile CommunityDocument { get; set; }
        public string MemberId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string Street { get; set; }
        public string AddressDescription { get; set; }
    }
    public class UpdateCommunityRequestModel
    {
        public IFormFile CommunityDocument { get; set; }
        public string CommunityName { get; set; }
        public string CommunityEmail { get; set; }
        public string CommunityPhoneNumber { get; set; }
    }
    public class CommunityResponseModel : BaseResponse
    {
        public CommunityDto Data { get; set; }
    }
    public class CommunitiesResponseModel : BaseResponse
    {
        public ICollection<CommunityDto> Data { get; set; } = new HashSet<CommunityDto>();
    }
    public class SearchRequest
    {
        public string State { get; set; }
        public string Street { get;set; }
        public string LGA { get; set; }
    }
}
