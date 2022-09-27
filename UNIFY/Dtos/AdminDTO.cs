using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace UNIFY.Dtos
{
    public class AdminDTO
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        
    }
    public class AdminRequestModel
    {
        public IFormFile Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
    }
    public class UpdateAdminRequestModel
    {
        public IFormFile Image { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
       
    }
    public class AdminResponseModel : BaseResponse
    {
        public AdminDTO Data { get; set; }
    }
    public class AdminsResponseModel : BaseResponse
    {
        public ICollection<AdminDTO> Data { get; set; } = new HashSet<AdminDTO>();
    }
}

