using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UNIFY.Dtos;
using UNIFY.Email;
using UNIFY.Interfaces.Repository;
using UNIFY.Interfaces.Services;
using UNIFY.Model.Entities;
using static UNIFY.Email.EmailDto;

namespace UNIFY.Implementation.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepository;
        private readonly IMemberRepository _memberRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _email;
        private readonly IWebHostEnvironment _webpost;
        public CommunityService(IEmailSender email, IWebHostEnvironment webpost, IUserRepository userRepository, IMemberRepository memberRepository, ICommunityRepository communityRepository, IAddressRepository addressRepository)
        {
            _email = email;
            _communityRepository = communityRepository;
            _addressRepository = addressRepository;
            _memberRepository = memberRepository;
            _userRepository = userRepository;
            _webpost = webpost;
        }
        public async Task<BaseResponse> ApproveCommunity(string id)
        {
            var community = await _communityRepository.GetCommunityInfo(id);
            var member = await _memberRepository.GetMemberWithUser(community.MemberId);
            if (community == null)
            {
                return new BaseResponse
                {
                    Message = "Community not found",
                    Status = false
                };
            }
            community.IsApproved = true;
            await _communityRepository.Update(community);
            //var emailRequest = new EmailRequestModel
            //{
            //    ReceiverName = member.User.UserName,
            //    ReceiverEmail = member.User.Email,
            //    Message = "Your Community has been approved, you can now view your community page",
            //    Subject = "Community Approval"
            //};
            //await _email.SendEmail(emailRequest);
            return new BaseResponse
            {
                Status = true,
                Message = "Community approved"
            };
        }

        public async Task<BaseResponse> DisApproveCommunity(string id)
        {
            var community = await _communityRepository.GetCommunityInfo(id);
            if (community == null)
            {
                return new BaseResponse
                {
                    Message = "Community not found",
                    Status = false
                };
            }
            community.IsApproved = false;
            await _communityRepository.Update(community);
            return new BaseResponse
            {
                Status = true,
                Message = "Community disapproved"
            };
        }

        public async Task<CommunitiesResponseModel> GetAllCommunities()
        {
            var communities = await _communityRepository.GetAllCommunities();
            if (communities == null)
            {
                return new CommunitiesResponseModel
                {
                    Message = "No Community Found",
                    Status = false
                };
            }
            var communityDto = communities.Select(communities => new CommunityDto
            {
                Id = communities.Id,
                AddressDescription = communities.Address.Street,
                State = communities.Address.State,
                LGA = communities.Address.LocalGovernment,
                Country = communities.Address.Country,
                CommunityName = communities.CommunityName,
                CommunityPhoneNumber = communities.CommunityPhoneNumber

            }).ToList();
            return new CommunitiesResponseModel
            {
                Data = communityDto,
                Message = "List Of All Communities",
                Status = true
            };
        }
        public async Task<CommunityResponseModel> GetCommunityById(string id)
        {
            var community = await _communityRepository.GetCommunityInfo(id);
            if (community == null)
            {
                return new CommunityResponseModel
                {
                    Message = "Community not found",
                    Status = false
                };
            }
            var communityDto = new CommunityDto
            {
                Id = community.Id,
                AddressDescription = community.Address.Street,
                State = community.Address.State,
                LGA = community.Address.LocalGovernment,
                Country = community.Address.Country,
                CommunityName = community.CommunityName,
                CommunityPhoneNumber = community.CommunityPhoneNumber
            };
            return new CommunityResponseModel
            {
                Data = communityDto,
                Message = "Community Successfully Retrieved",
                Status = true
            };
        }

        public async Task<CommunitiesResponseModel> GetApprovedCommunities()
        {
            
            var communities = await _communityRepository.GetApprovedCommunitiess();
            var communityDto = communities.Select(communities => new CommunityDto
            {
                Id = communities.Id,
                AddressDescription = communities.Address.Street,
                State = communities.Address.State,
                LGA = communities.Address.LocalGovernment,
                Country = communities.Address.Country,
                CommunityName = communities.CommunityName,
                CommunityPhoneNumber = communities.CommunityPhoneNumber
            }).ToList();

            return new CommunitiesResponseModel
            {
                Data = communityDto,
                Message = "List of all Approved Community",
                Status = true
            };
        }

        public async Task<CommunitiesResponseModel> GetCommunitiesByLGA(string Lga)
        {
            var communities = await _communityRepository.GetCommunitiessByLGA(Lga);
            if (communities == null)
            {
                return new CommunitiesResponseModel
                {
                    Message = "No Community available in this Local Goverment",
                    Status = false
                };
            }
            var communityDtos = communities.Select(communities => new CommunityDto
            {
                Id = communities.Id,
                AddressDescription = communities.Address.Street,
                State = communities.Address.State,
                LGA = communities.Address.LocalGovernment,
                Country = communities.Address.Country,
                CommunityName = communities.CommunityName,
                CommunityPhoneNumber = communities.CommunityPhoneNumber
            }).ToList();

            return new CommunitiesResponseModel
            {
                Data = communityDtos,
                Message = "List of all community under this localgovernment",
                Status = true
            };
        }

        public async Task<CommunitiesResponseModel> GetCommunitiesByStreet(string street)
        {
            var communities = await _communityRepository.GetCommunitiesByStreet(street);
            if (communities == null)
            {
                return new CommunitiesResponseModel
                {
                    Message = "No Community available in this Street",
                    Status = false
                };
            }
            var communityDtos = communities.Select(communities => new CommunityDto
            {
                Id = communities.Id,
                AddressDescription = communities.Address.AddressDescription,
                State = communities.Address.State,
                LGA = communities.Address.LocalGovernment,
                Country = communities.Address.Country,
                CommunityName = communities.CommunityName,
                CommunityPhoneNumber = communities.CommunityPhoneNumber
            }).ToList();

            return new CommunitiesResponseModel
            {
                Data = communityDtos,
                Message = "List of all community under this street",
                Status = true
            };
        }

        public async Task<CommunitiesResponseModel> GetUnApprovedCommunities()
        {
            var communities = await _communityRepository.GetUnApprovedCommunities();
            var communityDto = communities.Select(communities => new CommunityDto
            {
                Id = communities.Id,
                AddressDescription = communities.Address.Street,
                State = communities.Address.State,
                LGA = communities.Address.LocalGovernment,
                Country = communities.Address.Country,
                CommunityName = communities.CommunityName,
                CommunityPhoneNumber = communities.CommunityPhoneNumber
            }).ToList();

            return new CommunitiesResponseModel
            {
                Data = communityDto,
                Message = "List of all Un-Approved Community",
                Status = true
            };
        }

        public async Task<CommunityResponseModel> RegisterCommunity(CommunityRequestModel model)
        {
            var user = await _userRepository.Get(x => x.Id == model.MemberId);
            var member = await _memberRepository.Get(x => x.UserId == user.Id);
            if (model == null)
            {
                return new CommunityResponseModel
                {
                    Message = "Value can't be null",
                    Status = true
                };
            }
            var address = new Address
            {
                Country = model.Country,
                State = model.State,
                LocalGovernment = model.LGA,
                Street = model.Street,
            };
            var addAddress = await _addressRepository.Register(address);
            var community1 = new Community
            {
                IsApproved = false,
                CommunityName = model.CommunityName,
                CommunityPhoneNumber = model.CommunityPhoneNumber,
                AddressId = addAddress.Id
            };
            var addcommunity = await _communityRepository.Register(community1);
            community1.CreatedBy = addcommunity.Id;
            community1.LastModifiedBy = addcommunity.Id;
            community1.IsDeleted = false;
            var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (model.CommunityDocument != null)
            {

                var fileName = Path.GetFileNameWithoutExtension(model.CommunityDocument.FileName);
                var filePath = Path.Combine(folderPath, model.CommunityDocument.FileName);
                var extension = Path.GetExtension(model.CommunityDocument.FileName);
                if (!System.IO.Directory.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.CommunityDocument.CopyToAsync(stream);
                    }
                    community1.CommunityDocument = fileName;
                }
            }
            var communityDto = new CommunityDto
            {
                IsApproved = false,
                Id = addcommunity.Id,
                CommunityName = addcommunity.CommunityName,
                CommunityPhoneNumber = addcommunity.CommunityPhoneNumber,
                AddressDescription = addcommunity.Address.Street,
                State = addcommunity.Address.State,
                LGA = addcommunity.Address.LocalGovernment,
                Country = addcommunity.Address.Country,
                MemberId = addcommunity.MemberId

            };
            //var emailRequest = new EmailRequestModel
            //{
            //    ReceiverName = member.User.UserName,
            //    ReceiverEmail = member.User.Email,
            //    Message = "You have successfully registered your Community",
            //    Subject = "Community Registration"
            //};
            //await _email.SendEmail(emailRequest);
            return new CommunityResponseModel
            {
                Data = communityDto,
                Message = "Community Successfully Created",
                Status = true
            };
        }

        public async Task<BaseResponse> UpdateCommunity(UpdateCommunityRequestModel model, string Id)
        {
            var community = await _communityRepository.Get(x => x.Id == Id);
            if (community == null)
            {
                return new BaseResponse
                {
                    Status = false,
                    Message = "Community not found"
                };
            }
            var folderPath = Path.Combine(Directory.GetCurrentDirectory() + "\\Images\\");
            if (!System.IO.Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            if (model.CommunityDocument != null)
            {

                var fileName = Path.GetFileNameWithoutExtension(model.CommunityDocument.FileName);
                var filePath = Path.Combine(folderPath, model.CommunityDocument.FileName);
                var extension = Path.GetExtension(model.CommunityDocument.FileName);
                if (!System.IO.Directory.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.CommunityDocument.CopyToAsync(stream);
                    }
                    community.CommunityDocument = fileName;
                }
            }
            community.CommunityDocument = model.CommunityDocument.ToString() ?? community.CommunityDocument;
            community.CommunityName = model.CommunityName ?? community.CommunityName;
            community.CommunityPhoneNumber = model.CommunityPhoneNumber ?? community.CommunityPhoneNumber;
            await _communityRepository.Update(community);
            return new BaseResponse
            {
                Status = true,
                Message = "Community successfully update"
            };
        }

        public async Task<CommunityResponseModel> GetCommunityInfo(string id)
        {
            var community = await _communityRepository.GetCommunityInfo(id);
            if (community == null)
            {
                return new CommunityResponseModel
                {
                    Message = "Community not found",
                    Status = false
                };
            }
            var communityDto = new CommunityDto
            {
               
            };
            return new CommunityResponseModel
            {
                Message = "Community Successfully retrieved",
                Status = true,
                Data = communityDto
            };
        }

        public async Task<CommunitiesResponseModel> GetCommunitiesByState(string state)
        {
            var communities = await _communityRepository.GetCommunitiesByState(state);
            if (communities == null)
            {
                return new CommunitiesResponseModel
                {
                    Message = "No Community available in this State",
                    Status = false
                };
            }
            var communityDtos = communities.Select(communities => new CommunityDto
            {
                Id = communities.Id,
                AddressDescription = communities.Address.AddressDescription,
                State = communities.Address.State,
                LGA = communities.Address.LocalGovernment,
                Country = communities.Address.Country,
                CommunityName = communities.CommunityName,
                CommunityPhoneNumber = communities.CommunityPhoneNumber
            }).ToList();

            return new CommunitiesResponseModel
            {
                Data = communityDtos,
                Message = "List of all community under this State",
                Status = true
            };
        }
    }
}
