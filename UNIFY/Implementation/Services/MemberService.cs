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
using static UNIFY.Email.EmailDto;

namespace UNIFY.Implementation.Services
{
    public class MemberService : IMemberService
    {
        private readonly IEmailSender _email;
        private readonly IMemberRepository _memberRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IWebHostEnvironment _webHost;
       
        public MemberService(IWebHostEnvironment webHost, IEmailSender email, IMemberRepository memberRepository, IRoleRepository roleRepository, IAddressRepository addressRepository, IUserRepository userRepository)
        {
            _email = email;
            _memberRepository = memberRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _roleRepository = roleRepository;
            _webHost = webHost;
        }
        public async Task<BaseResponse> RegisterMember(MemberRequestModel model)
        {

            var member = await _userRepository.Get(a => a.Email == model.Email);
            if (member != null)
            {
                return new BaseResponse
                {
                    Message = "Email already exist",
                    Status = false
                };
            }
            else if (model != null)
            {
                var user = new User
                {
                    UserName = $"{model.FirstName} {model.LastName}",
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Email = model.Email
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
                var members = new Member
                {

                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Skills = model.Skills,
                    UserId = addUser.Id,
                    AddressId = addAddress.Id,
                   
                };
                var addMember = await _memberRepository.Register(members);
                addMember.CreatedBy = addMember.Id;
                addMember.LastModifiedBy = addMember.Id;
                addMember.IsDeleted = false;
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
                        members.Image = fileName;
                    }
                }
                await _memberRepository.Update(addMember);
                //var emailRequest = new EmailRequestModel
                //{
                //    ReceiverName = addUser.UserName,
                //    ReceiverEmail = addUser.Email,
                //    Message = "You have successfully registered as a Member on UNIFY, We provide a peaceful abode, for your satisfaction. Pls wait for your verification from the admin",
                //    Subject = "Members Registration"
                //};
                //await _email.SendEmail(emailRequest);
                return new BaseResponse
                {
                    Message = "Member Succesfully registered",
                    Status = true
                };

            }
            return new BaseResponse
            {
                Message = "Value cannot not be null",
                Status = false
            };
        }


        public async Task<BaseResponse> UpdateMember(UpdateMemberRequestModel model, string id)
        {
            if (model == null)
            {
                return new BaseResponse
                {
                    Message = "Value most not be null",
                    Status = false
                };
            }
            var member = await _memberRepository.GetMemberInfo(id);
            if (member == null)
            {
                return new BaseResponse
                {
                    Message = "Member not found",
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
                    member.Image = member.Image??fileName;
                }
            }
            member.User.UserName = $"{model.FirstName} {model.LastName}" ?? member.User.UserName;
            member.User.Email = model.Email ?? member.User.Email;
            member.Address.Country = model.Country ?? member.Address.Country;
            member.Address.State = model.State ?? member.Address.State;
            member.Address.LocalGovernment = model.LGA ?? member.Address.LocalGovernment;
            member.Address.AddressDescription = model.AddressDescription ?? member.Address.AddressDescription;
            member.FirstName = model.FirstName ?? member.FirstName;
            member.LastName = model.LastName ?? member.LastName;
            member.PhoneNumber = model.PhoneNumber ?? member.PhoneNumber;
            member.Skills = model.Skills ?? model.Skills;
            await _memberRepository.Update(member);

            return new BaseResponse
            {
                Message = "Member Succesfully Updated",
                Status = true
            };
        }


        public async Task<MembersResponseModel> GetMemberById(string id)
        {
            var member = await _memberRepository.GetMemberWithUser(id);
            if (member == null)
            {
                return new MembersResponseModel
                {
                    Message = "Member not found",
                    Status = false
                };
            }
            var memberDto = new MemberDto
            {
                Id = member.Id,
                Image = member.Image,
                FullName = $"{member.FirstName} {member.LastName}",
                PhoneNumber = member.PhoneNumber,
                Email = member.User.Email,
                Skills = member.Skills,
                State = member.Address.State,
                LGA = member.Address.LocalGovernment,
                AddressDescription = member.Address.AddressDescription,
                Country = member.Address.Country
            };
            return new MembersResponseModel
            {
                Message = "Member Successfully retrieved",
                Status = true,
                Data = memberDto
            };
        }


        public async Task<MemberResponseModel> GetAllMembers()
        {
            var members = await _memberRepository.GetAllMemberWithUser();
            var memberDto = members.Select(x => new MemberDto
            {
                Id = x.Id,
                Image = x.Image,
                FullName = $"{x.FirstName} {x.LastName}",
                PhoneNumber = x.PhoneNumber,
                Email = x.User.Email,
                Skills = x.Skills,
                State = x.Address.State,
                LGA = x.Address.LocalGovernment,
                AddressDescription = x.Address.AddressDescription,
                Country = x.Address.Country
            }).ToList();

            return new MemberResponseModel
            {
                Data = memberDto,
                Message = "List of all Members",
                Status = true
            };
        }


        public async Task<BaseResponse> VerifyMember(string id)
        {
            var member = await _memberRepository.GetMemberWithUser(id);
            member.IsVerified = true;
            var updatemember = await _memberRepository.Update(member);
            if (updatemember != null)
            {
                return new BaseResponse
                {
                    Message = "Member Verified",
                    Status = true
                };
            }
            //var emailRequest = new EmailRequestModel
            //{
            //    ReceiverName = member.User.UserName,
            //    ReceiverEmail = member.User.Email,
            //    Message = "You have been veriied by the admin",
            //    Subject = "Verification Message"
            //};
            //await _email.SendEmail(emailRequest);
            return new BaseResponse
            {
                Message = "Verification successful",
                Status = false
            };
        }


        public async Task<MemberResponseModel> GetAllVerifiedMembers()
        {
            var members = await _memberRepository.GetAllVerifiedMembers();
            var memberDto = members.Select(x => new MemberDto
            {
                Id = x.Id,
                Image = x.Image,
                FullName = $"{x.FirstName} {x.LastName}",
                PhoneNumber = x.PhoneNumber,
                Email = x.User.Email,
                State = x.Address.State,
                Skills = x.Skills,
                LGA = x.Address.LocalGovernment,
                AddressDescription = x.Address.AddressDescription,
                Country = x.Address.Country
            }).ToList();

            return new MemberResponseModel
            {
                Data = memberDto,
                Message = "List of all Members",
                Status = true
            };
        }


        public async Task<MemberResponseModel> GetNotVerifiedMembers()
        {
            var members = await _memberRepository.GetNotVerifiedMembers();
            var memberDto = members.Select(x => new MemberDto
            {
                Id = x.Id,
                Image = x.Image,
                FullName = $"{x.FirstName} {x.LastName}",
                PhoneNumber = x.PhoneNumber,
                Email = x.User.Email,
                State = x.Address.State,
                Skills = x.Skills,
                LGA = x.Address.LocalGovernment,
                AddressDescription = x.Address.AddressDescription,
                Country = x.Address.Country
            }).ToList();

            return new MemberResponseModel
            {
                Data = memberDto,
                Message = "List of all Member",
                Status = true
            };
        }

       

        public async Task<MembersResponseModel> GetMemberInfo(string id)
        {
            var member = await _memberRepository.GetMemberInfo(id);
            if (member == null)
            {
                return new MembersResponseModel
                {
                    Message = "Member not found",
                    Status = false
                };
            }
            var memberDto = new MemberDto
            {
                Id = member.Id,
                Image = member.Image,
                FullName = $"{member.FirstName} {member.LastName}",
                PhoneNumber = member.PhoneNumber,
                Email = member.User.Email,
                Skills = member.Skills,
                State = member.Address.State,
                LGA = member.Address.LocalGovernment,
                AddressDescription = member.Address.AddressDescription,
                Country = member.Address.Country
            };
            return new MembersResponseModel
            {
                Message = "Member Successfully retrieved",
                Status = true,
                Data = memberDto
            };
        }

        public async Task<CommunitiesResponseModel> GetCommunitiesByMemberId(string id)
        {
            var member = await _memberRepository.GetMemberInfo(id);
            var mem = await _memberRepository.GetCommunitiesByMember(member.Id);
            if (mem == null)
            {
                return new CommunitiesResponseModel
                {
                    Message = "Member not found",
                    Status = true
                };
            }
            var communityDto = mem.Select(community => new CommunityDto
            {
                Id = community.Id,
                CommunityName = community.CommunityName,
                CommunityPhoneNumber = community.CommunityPhoneNumber,
                
               
            }).ToList();

            return new CommunitiesResponseModel
            {
                Data = communityDto,
                Message = "All members of Community",
                Status = true
            };
        }
    }
}
