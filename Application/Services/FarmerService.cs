using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;
using Domain.Enum;

namespace Application.Services
{
    public class FarmerService : IFarmerService
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProduceTypeRepository _produceTypeRepository;
        private readonly IFileUploadServiceForWWWRoot _fileUploadServiceForWWWRoot;
        private readonly IFarmerProduceTypeRepository _farmerProduceTypeRepository;
        private readonly IMailService _mailService;



        public FarmerService(IFarmerRepository farmerRepository, IRoleRepository roleRepository, IUserRepository userRepository, IFileUploadServiceForWWWRoot fileUploadServiceForWWWRoot, IProduceTypeRepository produceTypeRepository, IFarmerProduceTypeRepository farmerProduceTypeRepository, IMailService mailService)
        {
            _farmerRepository = farmerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _fileUploadServiceForWWWRoot = fileUploadServiceForWWWRoot;
            _produceTypeRepository = produceTypeRepository;
            _farmerProduceTypeRepository = farmerProduceTypeRepository;
            _mailService = mailService;
        }


        public async Task<BaseResponse<FarmerDto>> RegisterFarmerAsync(CreateFarmerRequestModel model)
        {
            var farmerExist = await _farmerRepository.GetAsync(c => c.User.Email == model.Email);
            if (farmerExist != null)
            {
                return new BaseResponse<FarmerDto>
                {
                    Message = $"Email '{farmerExist.User.Email}' already exists",
                    Status = false,
                };
            }
            var phoneNumer = await _farmerRepository.GetAsync(c => c.User.PhoneNumber == model.PhoneNumber);
            if (phoneNumer != null)
            {
                return new BaseResponse<FarmerDto>
                {
                    Message = $"phone Number '{phoneNumer.User.PhoneNumber}' already exists",
                    Status = false,
                };
            }

            var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);

            var role = await _roleRepository.GetAsync(a => a.RoleName.ToLower() == "farmer".ToLower());

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                // ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(model.ConfirmPassword),
                Address = model.Address,
                Email = model.Email,
                ProfilePicture = profilePicture,
                Gender = model.Gender,
                Role = role,
                RoleId = role.Id,
                //CreatedBy = loginId,
            };

            await _userRepository.CreateAsync(user);

            var farmer = new Farmer
            {
                //CreatedBy = loginId,
                RegistrationNumber = await GeneratefarmerRegNumAsync(),
                FarmName = model.FarmName,
                UserId = user.Id,
                // User = user,
                BankName = model.BankName,
                AccountName = model.AccountName,
                AccountNumber = model.AccountNumber,
            };

            foreach (var item in model.ProduceTypes)
            {
                var produceType = await _produceTypeRepository.GetAsync(pt => pt.TypeName == item);
                var farmerProduceType = new FarmerProduceType
                {
                    FarmerId = farmer.Id,
                    ProduceTypeId = produceType.Id
                };
                await _farmerProduceTypeRepository.CreateAsync(farmerProduceType);
            }

            await _farmerRepository.CreateAsync(farmer);
            await _farmerRepository.SaveAsync();

            var userFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";
            var userFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.LastName)}";
            var fullName = userFirstLetterOfFirstNameToUpperCase + " " + userFirstLetterOfLastNameToUpperCase;

            var emailSender = new EmailSenderDetails
            {
                Subject = "Registration Status",
                ReceiverEmail = user.Email,
                ReceiverName = model.FirstName,
                HtmlContent = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title></head><body><h4>Hello, welcome on board</h4></body></html>"
            };
            await _mailService.EmailVerificationTemplate(emailSender);

            return new BaseResponse<FarmerDto>
            {
                Message = $"Dear {fullName}, you will receive a notification through your registered email for the status of your application, thanks",
                Status = true,
                Data = null,

            };
        }



        public async Task<BaseResponse<FarmerDto>> DeleteFarmerAsync(Guid id)
        {
            var farmer = await _farmerRepository.GetAsync(d => d.Id == id);
            if (farmer != null)
            {
                farmer.IsDeleted = true;
                farmer.User.IsDeleted = true;
                _farmerRepository.Update(farmer);
                await _farmerRepository.SaveAsync();

                return new BaseResponse<FarmerDto>
                {
                    Message = "Successful",
                    Status = true
                };
            }
            
            return new BaseResponse<FarmerDto>
            {
                Message = "farmer does not exist",
                Status = false
            };
        }

        public async Task<BaseResponse<FarmerDto>> GetFarmerAcountDetailsByIdAsync(Guid id)
        {
            var farmer = await _farmerRepository.GetAsync(f => f.Id == id);
            if (farmer == null)
            {
                return new BaseResponse<FarmerDto>
                {
                    Message = "Farmer Not Found",
                    Status = false
                };
            }

            var farmerFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmer.User.FirstName)}";
            var farmerFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmer.User.LastName)}";
            var fullName = farmerFirstLetterOfFirstNameToUpperCase + " " + farmerFirstLetterOfLastNameToUpperCase;
           
            return new BaseResponse<FarmerDto>
            {
                Message = $"Account details of {fullName} found",
                Status = true,
                Data = new FarmerDto
                {
                    BankName = farmer.BankName,
                    AccountName = farmer.AccountName,
                    AccountNumber = farmer.AccountNumber
                }
            };

        }


        public async Task<BaseResponse<FarmerProduceTypeDto>> GetFarmerAlongWithRegisteredProduceTypeAsync(Guid id)
        {
            var farmer = await _farmerProduceTypeRepository.GetAsync(id);
            if (farmer.Any())
            {

                return new BaseResponse<FarmerProduceTypeDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new FarmerProduceTypeDto
                    {
                         FarmerDto = new FarmerDto
                        {
                            Id = farmer[0].Farmer.Id,
                            RegistrationNumber = farmer[0].Farmer.RegistrationNumber,
                            FirstName = farmer[0].Farmer.User.FirstName,
                            LastName = farmer[0].Farmer.User.LastName,
                            Email = farmer[0].Farmer.User.Email,
                            PhoneNumber = farmer[0].Farmer.User.PhoneNumber,
                            ProfilePicture = farmer[0].Farmer.User.ProfilePicture,
                            Address = farmer[0].Farmer.User.Address,
                            FarmName = farmer[0].Farmer.FarmName,
                            FarmerRegStatus = farmer[0].Farmer.FarmerRegStatus,

                        },
                        ProduceTypeDto = farmer.Select(pt => new ProduceTypeDto
                        {
                            Id = pt.Id,
                            TypeName = pt.ProduceType.TypeName,
                            ProduceName = pt.ProduceType.Produce.ProduceName,
                            NameOfCategory = pt.ProduceType.Produce.Category.NameOfCategory,
                        }).ToList()

                    },
                };
            }
            return new BaseResponse<FarmerProduceTypeDto>
            {
                Message = "farmer is not found",
                Status = false
            };
        }

        public async Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllFarmersAsync()
        {
            var farmers = await _farmerRepository.GetAllAsync();
            if (!farmers.Any())
            {
                return new BaseResponse<IEnumerable<FarmerDto>>
                {
                    Message = "No farmer found",
                    Status = false
                };
            }

            return new BaseResponse<IEnumerable<FarmerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = farmers.Select(m => new FarmerDto
                {
                    // Id = m.Id,
                    RegistrationNumber = m.RegistrationNumber,
                    FirstName = m.User.FirstName,
                    LastName = m.User.LastName,
                    Email = m.User.Email,
                    PhoneNumber = m.User.PhoneNumber,
                    Address = m.User.Address,
                    FarmName = m.FarmName,

                    // ProfilePicture = m.User.ProfilePicture,
                })
            };
        }



        public async Task<BaseResponse<FarmerDto>> UpdateFarmerAsync(Guid id, UpdateFarmerRequestModel model)
        {
            var farmer = await _farmerRepository.GetAsync(a => a.Id == id);
            if (farmer is not null)
            {
                if (model.ProfilePicture != null)
                {
                    var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);
                    farmer.User.ProfilePicture = profilePicture;
                }

                farmer.User.Address = model.Address;
                farmer.User.PhoneNumber = model.PhoneNumber;
                farmer.User.Password = model.Password;


                _farmerRepository.Update(farmer);
                await _farmerRepository.SaveAsync();

                return new BaseResponse<FarmerDto>
                {
                    Message = "Profile Updated Successfully",
                    Status = true,
                    Data = new FarmerDto
                    {
                        ProfilePicture = farmer.User.ProfilePicture,
                        PhoneNumber = farmer.User.PhoneNumber,
                        Address = farmer.User.Address,
                        Password = farmer.User.Password,
                    }
                };
            }
            return new BaseResponse<FarmerDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }




        public async Task<BaseResponse<string>> VerifyFarmerAsync(ApproveFarmerDto model)
        {
            var farmer = await _farmerRepository.GetAsync(model.Id);

            if (farmer == null)
            {
                return new BaseResponse<string>
                {
                    Message = "Farmer not found",
                    Status = false,
                };
            }

            farmer.FarmerRegStatus = model.Status;
            _farmerRepository.Update(farmer);
            await _farmerRepository.SaveAsync();

            return new BaseResponse<string>
            {
                Message = "Successful",
                Status = true
            };
        }


        public async Task<BaseResponse<IEnumerable<FarmerDto>>> GetFarmersByStatusAsync(FarmerStatusRequestModel model)
        {
            var farmers = await _farmerRepository.GetSelectedAsync(f => f.FarmerRegStatus == model.Status && !f.IsDeleted);

            if (!farmers.Any())
            {
                return new BaseResponse<IEnumerable<FarmerDto>>
                {
                    Message = "Farmer not found",
                    Status = false,
                };
            }

            return new BaseResponse<IEnumerable<FarmerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = farmers.Select(f => new FarmerDto
                {
                    Id = f.Id,
                    RegistrationNumber = f.RegistrationNumber,
                    FirstName = f.User.FirstName,
                    LastName = f.User.LastName,
                    Email = f.User.Email,
                    PhoneNumber = f.User.PhoneNumber,
                    Address = f.User.Address,
                    ProfilePicture = f.User.ProfilePicture,
                })
            };
        }

       
        private async Task<string> GeneratefarmerRegNumAsync()
        {
            var count = (await _farmerRepository.GetAllAsync()).Count();
            return "EP/FAR/" + $"{count + 1}";
        }



        public Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllAsync(Func<FarmerDto, bool> expression)
        {
            throw new NotImplementedException();
        }



    }
}