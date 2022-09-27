using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using UNIFY.Model.Entities;

namespace UNIFY.Dtos
{
    public class SecurityDto
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string SecurityId { get; set; }
        public string CommunityId { get; set; }
        public string SecurityFirstName { get; set; }
        public string SecurityLastName { get; set; }
        public string SecurityPhoneNumber { get; set; }
        public string SecurityEmail { get; set; }
        public string Password { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string LGA { get; set; }
        public string AddressDescription { get; set; }
        public string SecurityOrganization { get; set; }

        public class SecurityRequestModel
        {
            public IFormFile Image { get; set; }
            public string SecurityId { get; set; }
            public string CommunityId { get; set; }
            public string SecurityFirstName { get; set; }
            public string SecurityLastName { get; set; }
            public string SecurityPhoneNumber { get; set; }
            public string SecurityOrganization { get; set; }
            public string SecurityEmail { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string Password { get; set; }
            public string LGA { get; set; }
            public string AddressDescription { get; set; }
        }

        public class UpdateSecurityRequestModel
        {
            public IFormFile Image { get; set; }
            public string SecurityOrganization { get; set; }
            public string SecurityId { get; set; }
            public string CommunityId { get; set; }
            public string SecurityFirstName { get; set; }
            public string SecurityLastName { get; set; }
            public string Password { get; set; }
            public string SecurityPhoneNumber { get; set; }
            public string SecurityEmail { get; set; }
            public string Country { get; set; }
            public string State { get; set; }
            public string LGA { get; set; }
            public string AddressDescription { get; set; }
        }

        public class SecurityResponseModel : BaseResponse
        {
            public SecurityDto Data { get; set; }
        }
        public class SecuritiesResponseModel : BaseResponse
        {
            public ICollection<SecurityDto> Data { get; set; } = new HashSet<SecurityDto>();
        }
    }

}
