using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Dtos;
using UNIFY.Identity;
using UNIFY.Implementation.Repository;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;
using UNIFY.Model.Entities;

namespace UNIFY.Implementation.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IWebHostEnvironment _webHost;
        public AdminService(IWebHostEnvironment webHost, IAdminRepository adminRepository, IRoleRepository roleRepository, IAddressRepository addressRepository, IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _roleRepository = roleRepository;
            _webHost = webHost;
            
           
        }

        public async Task<BaseResponse> ActivateAdmin(string id)
        {
            var admin = await _adminRepository.Get(x => x.Id == id);
            if (admin == null)
            {
                return new BaseResponse
                {
                    Message = "admin not found",
                    Status = false
                };
            }
            admin.IsDeleted = true;
            await _adminRepository.Update(admin);
            return new BaseResponse
            {
                Message = "admin Successfully activate",
                Status = true
            };
        }

        public async Task<BaseResponse> DeActivateAdmin(string id)
        {
            var admin = await _adminRepository.Get(x => x.Id == id);
            if (admin == null)
            {
                return new AdminResponseModel
                {
                    Message = "admin not found",
                    Status = false
                };
            }
            admin.IsDeleted = false;
            await _adminRepository.Update(admin);
            return new AdminResponseModel
            {
                Message = "admin Successfully deactivate",
                Status = true
            };
        }

        public async Task<AdminResponseModel> GetAdminInfo(string id)
        {
            var admin = await _adminRepository.GetAdminInfo(id);
            if (admin == null)
            {
                return new AdminResponseModel
                {
                    Message = "admin not found",
                    Status = false
                };
            }
            var adminDto = new AdminDTO
            {
                Id = admin.Id,
                Image = admin.Image,
                FullName = $"{admin.FirstName} {admin.LastName}",
                PhoneNumber = admin.PhoneNumber,
                Email = admin.User.Email,
            };
            return new AdminResponseModel
            {
                Message = "admin Successfully retrieved",
                Status = true,
                Data = adminDto
            };
        }

        public async Task<AdminsResponseModel> GetAllActivatedAdmins()
        {
            var admins = await _adminRepository.GetAllActivatedAdmin();
            var adminDto = admins.Select(x => new AdminDTO
            {
                Id = x.Id,
                Image = x.Image,
                FullName = $"{x.FirstName} {x.LastName}",
                PhoneNumber = x.PhoneNumber,
                Email = x.User.Email,
            }).ToList();

            return new AdminsResponseModel
            {
                Data = adminDto,
                Message = "List of all activated admins",
                Status = true
            };
        }

        public async Task<AdminsResponseModel> GetAllAdmins()
        {
            var admins = await _adminRepository.GetAllAdminWithUser();
            var adminDto = admins.Select(x => new AdminDTO
            {
                Id = x.Id,
                Image = x.Image,
                FullName = $"{x.FirstName} {x.LastName}",
                PhoneNumber = x.PhoneNumber,
                Email = x.User.Email,
            }).ToList();

            return new AdminsResponseModel
            {
                Data = adminDto,
                Message = "List of all admins",
                Status = true
            };
        }

        public async Task<AdminsResponseModel> GetAllDeactivatedAdmins()
        {
            var admins = await _adminRepository.GetAllActivatedAdmin();
            var adminDto = admins.Select(x => new AdminDTO
            {
                Id = x.Id,
                Image = x.Image,
                FullName = $"{x.FirstName} {x.LastName}",
                PhoneNumber = x.PhoneNumber,
                Email = x.User.Email,
            }).ToList();
            return new AdminsResponseModel
            {
                Data = adminDto,
                Message = "These are the List of activated admins",
                Status = true
            };
        }

        public async Task<BaseResponse> RegisterAdmin(AdminRequestModel model)
        {
            var land = await _userRepository.Get(a => a.Email == model.Email);
            if (land != null)
            {
                return new BaseResponse
                {
                    Message = "Email or password already exist",
                    Status = false,
                };
            }
            var user = new User
            {
                UserName = $"{model.FirstName} {model.LastName}",
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                Email = model.Email,
               
            };
            Console.WriteLine($"{user.Password}");

            var addUser = await _userRepository.Register(user);
           
            var role = await _roleRepository.Get(x => x.RoleName == "admin");
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
            var admin = new Admin
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserId = addUser.Id,
            };
            var addadmin = await _adminRepository.Register(admin);
            admin.CreatedBy = addadmin.Id;
            admin.LastModifiedBy = addadmin.Id;
            admin.IsDeleted = false;

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
                    admin.Image = fileName;
                }
            }
            await _adminRepository.Update(admin);
            return new BaseResponse
            {
                Message = "admin Successfully registered",
                Status = true,
            };
            return new BaseResponse
            {
                Message = "Value cannot be null ",
                Status = false,
            };
        }

        public async Task<BaseResponse> UpdateAdmin(UpdateAdminRequestModel model, string id)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Value most not be null",
                    Status = false
                };
            }
            var admin = await _adminRepository.Get(x => x.Id == id);
            if (admin == null)
            {
                return new BaseResponse
                {
                    Message = "admin not found",
                    Status = false
                };
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var fileName = Path.GetFileNameWithoutExtension(model.Image.FileName);
            var filePath = Path.Combine(folderPath, model.Image.FileName);
            var extension = Path.GetExtension(model.Image.FileName);
            if (!System.IO.Directory.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }
                admin.Image = fileName;
            }
            await _adminRepository.Update(admin);
            admin.PhoneNumber = model.PhoneNumber;
            admin.FirstName = model.FirstName;
            admin.LastName = model.LastName;
            var updateadmin = await _adminRepository.Update(admin);


            return new BaseResponse
            {
                Message = "admin Succesfully Updated",
                Status = true
            };
        }
    }
}
