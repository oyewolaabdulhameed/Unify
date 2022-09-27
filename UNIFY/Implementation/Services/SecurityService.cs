using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Dtos;
using UNIFY.Email;
using UNIFY.Identity;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;
using UNIFY.Model.Entities;
using static UNIFY.Dtos.SecurityDto;

namespace UNIFY.Implementation.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly ISecurityRepository _securityRepository;
        private readonly ICommunityRepository _communityRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailSender _email;
        private readonly IWebHostEnvironment _webpost;
        public SecurityService(IRoleRepository roleRepository, ISecurityRepository securityRepository,IEmailSender email, IWebHostEnvironment webpost, IUserRepository userRepository, IMemberRepository memberRepository, ICommunityRepository communityRepository, IAddressRepository addressRepository)
        {
            _roleRepository = roleRepository;
           _securityRepository = securityRepository;
            _email = email;
            _communityRepository = communityRepository;
            _addressRepository = addressRepository;
            _memberRepository = memberRepository;
            _userRepository = userRepository;
            _webpost = webpost;
        }
        public Task<BaseResponse> DeleteSecurity(string Id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SecurityDto.SecuritiesResponseModel> GetAllSecurities()
        {
            var securities = await _securityRepository.GetAllSecurityWithUser();
            var securityDto = securities.Select(x => new SecurityDto
            {
                Id = x.Id,
                Image = x.Image,
                SecurityOrganization = x.SecurityOrganization,
                SecurityPhoneNumber = x.SecurityPhoneNumber,
                SecurityEmail = x.SecurityEmail,
                State = x.Address.State,
                LGA = x.Address.LocalGovernment,
                AddressDescription = x.Address.AddressDescription,
                Country = x.Address.Country
            }).ToList();

            return new SecuritiesResponseModel
            {
                Data = securityDto,
                Message = "List of all Securities",
                Status = true
            };
        }

        public async Task<SecurityDto.SecurityResponseModel> GetSecurityById(string id)
        {
            var security = await _securityRepository.GetSecurityWithUser(id);
            if (security == null)
            {
                return new SecurityResponseModel
                {
                    Message = "Security not found",
                    Status = false
                };
            }
            var securityDto = new SecurityDto
            {
                Id = security.Id,
                Image = security.Image,
               SecurityFirstName = security.SecurityFirstName,
               SecurityLastName = security.SecurityLastName,
               SecurityPhoneNumber = security.SecurityPhoneNumber,
               SecurityEmail = security.SecurityEmail,
               SecurityOrganization = security.SecurityOrganization,
                State = security.Address.State,
                LGA = security.Address.LocalGovernment,
                AddressDescription = security.Address.AddressDescription,
                Country = security.Address.Country

            };
            return new SecurityResponseModel
            {
                Message = "Security Successfully retrieved",
                Status = true,
                Data = securityDto
            };
        }

        public async Task<BaseResponse> RegisterSecurity(SecurityDto.SecurityRequestModel model)
        {
            var security = await _securityRepository.Get(a => a.User.Email == model.SecurityEmail);
            if(security != null) 
            {
                return new BaseResponse
                {
                    Message = "Security already exist",
                    Status = false,
                };
            }
            var user = new User
            {
                UserName = $"{model.SecurityFirstName} {model.SecurityLastName}",
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Email = model.SecurityEmail,
            };
            var addUser = await _userRepository.Register(user);
            var role = await _roleRepository.Get(x => x.RoleName == "Member");
            if (role == null)
            {
                return new BaseResponse
                {
                    Message = "Role not found",
                    Status = false
                };
            }
            var userRole = new UserRole
            {
                UserId = addUser.Id,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);
            var updateUser = await _userRepository.Update(user);

            var address = new Address
            {
                Country = model.Country,
                State = model.State,
                LocalGovernment = model.LGA,
            };
            var addAddress = await _addressRepository.Register(address);
            var securities = new Security
            {
                SecurityFirstName = model.SecurityFirstName,
                SecurityLastName = model.SecurityLastName,
                SecurityEmail = model.SecurityEmail,
                SecurityPhoneNumber = model.SecurityPhoneNumber,
                SecurityOrganization = model.SecurityOrganization,
                UserId = addUser.Id,
                AddressId = addAddress.Id,
            };
            var addSecurity = await _securityRepository.Register(securities);
            addSecurity.CreatedBy = addSecurity.Id;
            addSecurity.LastModifiedBy = addSecurity.Id;
            addSecurity.IsDeleted = false;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (model.Image != null)
            {

                var fileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
                var filePath = Path.Combine(folderPath, model.Image.FileName);
                var extension = Path.GetExtension(model.Image.FileName);
                if (!System.IO.Directory.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    securities.Image = fileName;
                }
            }
            await _securityRepository.Update(addSecurity);
            //var emailRequest = new EmailRequestModel
            //{
            //    ReceiverName = addUser.UserName,
            //    ReceiverEmail = addUser.Email,
            //    Message = "You have successfully registered as a Security on UNIFY, We provide a peaceful abode, for your satisfaction. Pls wait for your verification from the admin",
            //    Subject = "Securities Registration"
            //};
            //await _email.SendEmail(emailRequest);
            return new BaseResponse
            {
                Message = "Security Succesfully registered",
                Status = true
            };
            return new BaseResponse
            {
                Message = "Value cannot not be null",
                Status = false
            };
        }


        public async Task<BaseResponse> UpdateSecurity(SecurityDto.UpdateSecurityRequestModel model, string id)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Value most not be null",
                    Status = false
                };
            }
            var security = await _securityRepository.GetSecurityInfo(id);
            if (security == null)
            {
                return new BaseResponse
                {
                    Message = "Security not found",
                    Status = false
                };
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (model.Image != null)
            {

                var fileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
                var filePath = Path.Combine(folderPath, model.Image.FileName);
                var extension = Path.GetExtension(model.Image.FileName);
                if (!System.IO.Directory.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }
                    security.Image = security.Image ?? fileName;
                }
            }
            security.User.UserName = $"{model.SecurityFirstName} {model.SecurityLastName}" ?? security.User.UserName;
            security.User.Email = model.SecurityEmail ?? security.User.Email;
            security.Address.Country = model.Country ?? security.Address.Country;
            security.Address.State = model.State ?? security.Address.State;
            security.Address.LocalGovernment = model.LGA ?? security.Address.LocalGovernment;
            security.Address.AddressDescription = model.AddressDescription ?? security.Address.AddressDescription;
            security.SecurityFirstName = model.SecurityFirstName ?? security.SecurityFirstName;
            security.SecurityLastName = model.SecurityLastName ?? security.SecurityLastName;
            security.SecurityPhoneNumber = model.SecurityPhoneNumber ?? security.SecurityPhoneNumber;
            await _securityRepository.Update(security);

            return new BaseResponse
            {
                Message = "Security Succesfully Updated",
                Status = true
            };
        }
    }
}
