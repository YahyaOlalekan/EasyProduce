using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ManagerService : IManagerService
    {

        private readonly IManagerRepository _managerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileUploadServiceForWWWRoot _fileUploadServiceForWWWRoot;

        public ManagerService(IManagerRepository managerRepository, IRoleRepository roleRepository, IUserRepository userRepository, IFileUploadServiceForWWWRoot fileUploadServiceForWWWRoot)
        {
            _managerRepository = managerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _fileUploadServiceForWWWRoot = fileUploadServiceForWWWRoot;
        }

        public async Task<BaseResponse<ManagerDto>> CreateAsync(CreateManagerRequestModel model)
        {
            var managerExist = await _managerRepository.GetAsync(m => m.User.Email == model.Email);
            //BaseEntity managerExist = await _managerRepository.GetAsync(m => m.User.Email == model.Email);
            if (managerExist != null)
            {
                return new BaseResponse<ManagerDto>
                {
                    Message = "Email already exists",
                    Status = false,
                };
            }
            var phoneNumer = await _managerRepository.GetAsync(m => m.User.PhoneNumber == model.PhoneNumber);
            if (phoneNumer != null)
            {
                return new BaseResponse<ManagerDto>
                {
                    Message = "Phone number already exists",
                    Status = false,
                };
            }

            var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);


            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(model.ConfirmPassword),
                Address = model.Address,
                Email = model.Email,
                ProfilePicture = profilePicture,
                Role = await _roleRepository.GetAsync(a => a.RoleName.ToLower() == "manager"),
                //CreatedBy = loginId,
            };

            var manager = new Manager
            {
                //CreatedBy = loginId,
                RegistrationNumber =  await GenerateManagerRegNumAsync(),
                UserId = user.Id,
                User = user,
            };
            await _userRepository.CreateAsync(user);
            await _managerRepository.CreateAsync(manager);
            await _managerRepository.SaveAsync();

         var userFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";


            return new BaseResponse<ManagerDto>
            {
                Message = $"{user.FirstName} is successfully added as Manager",
                Status = true,
                Data = new ManagerDto
                {
                    Id = manager.Id,
                    RegistrationNumber = manager.RegistrationNumber,
                    FirstName = manager.User.FirstName,
                    LastName = manager.User.LastName,
                    Email = manager.User.Email,
                    PhoneNumber = manager.User.PhoneNumber,
                    ProfilePicture = manager.User.ProfilePicture,
                }
            };
        }



        public async Task<BaseResponse<ManagerDto>> DeleteAsync(Guid id)
        {
            var manager = await _managerRepository.GetAsync(d => d.Id == id);
            if (manager != null)
            {
                manager.IsDeleted = true;
                manager.User.IsDeleted = true;
                _managerRepository.Update(manager);
                await _managerRepository.SaveAsync();

                return new BaseResponse<ManagerDto>
                {
                    Message = "successful",
                    Status = true
                };
            }
            return new BaseResponse<ManagerDto>
            {
                Message = "Manager does not exist",
                Status = false
            };
        }



        public async Task<BaseResponse<ManagerDto>> GetAsync(Guid id)
        {
            var manager = await _managerRepository.GetAsync(g => g.Id == id || g.UserId == id);
            if (manager != null)
            {

                return new BaseResponse<ManagerDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new ManagerDto
                    {
                        Id = manager.Id,
                        RegistrationNumber = manager.RegistrationNumber,
                        FirstName = manager.User.FirstName,
                        LastName = manager.User.LastName,
                        Email = manager.User.Email,
                        PhoneNumber = manager.User.PhoneNumber,
                        ProfilePicture = manager.User.ProfilePicture,
                        Address = manager.User.Address
                    },
                };
            }
            return new BaseResponse<ManagerDto>
            {
                Message = "Manager is not found",
                Status = false
            };
        }

        public async Task<BaseResponse<IEnumerable<ManagerDto>>> GetAllAsync()
        {
            var managers = await _managerRepository.GetAllAsync();
            if (managers == null)
            {
                return new BaseResponse<IEnumerable<ManagerDto>>
                {
                    Message = "No Manager found",
                    Status = false
                };
            }


            return new BaseResponse<IEnumerable<ManagerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = managers.Select(m => new ManagerDto
                {
                    Id = m.Id,
                    RegistrationNumber = m.RegistrationNumber,
                    FirstName = m.User.FirstName,
                    LastName = m.User.LastName,
                    Email = m.User.Email,
                    PhoneNumber = m.User.PhoneNumber,
                    Address = m.User.Address,
                    ProfilePicture = m.User.ProfilePicture,
                })
            };
        }



        public async Task<BaseResponse<ManagerDto>> UpdateAsync(Guid id, UpdateManagerRequestModel model)
        {
            var manager = await _managerRepository.GetAsync(a => a.UserId == id);
            if (manager is not null)
            {
                if (model.ProfilePicture != null)
                {
                    var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);

                    //var profilePicture = UploadFile(model.ProfilePicture);
                    manager.User.ProfilePicture = profilePicture;
                }

                manager.User.FirstName = model.FirstName;
                manager.User.Address = model.Address;
                manager.User.LastName = model.LastName;
                manager.User.Email = model.Email;
                // manager.User.Password = model.Password;
                manager.User.PhoneNumber = model.PhoneNumber;

                _managerRepository.Update(manager);
                await _managerRepository.SaveAsync();

                return new BaseResponse<ManagerDto>
                {
                    Message = "Manager Updated Successfully",
                    Status = true,
                    Data = new ManagerDto
                    {
                        ProfilePicture = manager.User.ProfilePicture,
                        FirstName = manager.User.FirstName,
                        LastName = manager.User.LastName,
                        PhoneNumber = manager.User.PhoneNumber,
                        Email = manager.User.Email,
                        Address = manager.User.Address,
                    }
                };
            }
            return new BaseResponse<ManagerDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }



        private async Task<string> GenerateManagerRegNumAsync()
        {
            var count = (await _managerRepository.GetAllAsync()).Count();
            return "EP/MAG/00" + $"{count + 1}";
        }

       
    }
}

