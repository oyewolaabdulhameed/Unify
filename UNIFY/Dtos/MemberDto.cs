using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace UNIFY.Dtos
{
    public class MemberDto
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string? Skills { get; set; }
        public string AddressDescription { get; set; }
        public string LGA { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
    public class MemberRequestModel
    {
        public IFormFile Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Skills { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string AddressDescription { get; set; }
    }
    public class UpdateMemberRequestModel
    {
        public IFormFile Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Skills { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? LGA { get; set; }
        public string? AddressDescription { get; set; }
    }
    public class MembersResponseModel : BaseResponse
    {
        public MemberDto Data { get; set; }
    }
    public class MemberResponseModel : BaseResponse
    {
        public ICollection<MemberDto> Data { get; set; } = new HashSet<MemberDto>();
    }
}
