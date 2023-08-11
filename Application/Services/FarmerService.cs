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

        public FarmerService(IFarmerRepository farmerRepository, IRoleRepository roleRepository, IUserRepository userRepository, IFileUploadServiceForWWWRoot fileUploadServiceForWWWRoot, IProduceTypeRepository produceTypeRepository, IFarmerProduceTypeRepository farmerProduceTypeRepository)
        {
            _farmerRepository = farmerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _fileUploadServiceForWWWRoot = fileUploadServiceForWWWRoot;
            _produceTypeRepository = produceTypeRepository;
            _farmerProduceTypeRepository = farmerProduceTypeRepository;
        }

        public async Task<BaseResponse<FarmerDto>> CreateAsync(CreateFarmerRequestModel model)
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
                User = user,
            };

            foreach (var item in model.produceTypes)
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

            return new BaseResponse<FarmerDto>
            {
                Message = $"Dear {fullName}, you will receive a notification through your registered email for the status of your application, thanks",
                Status = true,
                Data = null,

                // Data = new FarmerDto
                // {
                //     Id = farmer.Id,
                //     RegistrationNumber = farmer.RegistrationNumber,
                //     FirstName = farmer.User.FirstName,
                //     LastName = farmer.User.LastName,
                //     Email = farmer.User.Email,
                //     PhoneNumber = farmer.User.PhoneNumber,
                //     ProfilePicture = farmer.User.ProfilePicture,
                //     FarmName = farmer.FarmName,

                // }
            };
        }



        public async Task<BaseResponse<FarmerDto>> DeleteAsync(Guid id)
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



        public async Task<BaseResponse<FarmerProduceTypeDto>> GetAsync(Guid id)
        {
            var farmer = await _farmerProduceTypeRepository.GetAsync(id);
            if (farmer != null)
            {

                return new BaseResponse<FarmerProduceTypeDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new FarmerProduceTypeDto
                    {
                        farmerDto = new FarmerDto
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
                        },
                        produceTypeDto = farmer.Select(pt => new ProduceTypeDto
                        {
                            Id = pt.Id,
                            TypeName=pt.ProduceType.TypeName,
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

        public async Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllAsync()
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
                    Id = m.Id,
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



        public async Task<BaseResponse<FarmerDto>> UpdateAsync(Guid id, UpdateFarmerRequestModel model)
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



        private async Task<string> GeneratefarmerRegNumAsync()
        {
            var count = (await _farmerRepository.GetAllAsync()).Count();
            return "EP/FAR/" + $"{count + 1}";
        }



        public Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllAsync(Func<FarmerDto, bool> expression)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<FarmerDto>>> GetPendingFarmersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<FarmerDto>>> ApprovedFarmersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<FarmerDto>>> GetDeclinedFarmersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<FarmerDto>> VerifyFarmersAsync(ApproveFarmerDto model)
        {
            throw new NotImplementedException();
        }

        Task<BaseResponse<FarmerDto>> IFarmerService.GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}