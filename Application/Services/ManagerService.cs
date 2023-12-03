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
                    Message = $"Email '{managerExist.User.Email}' already exists",
                    Status = false,
                };
            }
            var phoneNumer = await _managerRepository.GetAsync(m => m.User.PhoneNumber == model.PhoneNumber);
            if (phoneNumer != null)
            {
                return new BaseResponse<ManagerDto>
                {
                    Message = $"phone Numer '{phoneNumer.User.PhoneNumber}' already exists",
                    Status = false,
                };
            }

            var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);

            var role = await _roleRepository.GetAsync(a => a.RoleName.ToLower() == "manager".ToLower());

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                // ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(model.ConfirmPassword),
                Address = model.Address,
                Email = model.Email,
                ProfilePicture = profilePicture.name,
                Gender = model.Gender,
                Role = role,
                RoleId = role.Id,
                //CreatedBy = loginId,
            };

            await _userRepository.CreateAsync(user);

            var manager = new Manager
            {
                //CreatedBy = loginId,
                RegistrationNumber = await GenerateManagerRegNumAsync(),
                UserId = user.Id,
                User = user,
            };

            await _managerRepository.CreateAsync(manager);
            await _managerRepository.SaveAsync();

            var userFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";
            var userFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.LastName)}";
            var fullName = userFirstLetterOfFirstNameToUpperCase + " " + userFirstLetterOfLastNameToUpperCase;

            return new BaseResponse<ManagerDto>
            {
                Message = $"{fullName} is successfully registered as a Manager",
                Status = true,
                Data = null,

                // Data = new ManagerDto
                // {
                //     Id = manager.Id,
                //     RegistrationNumber = manager.RegistrationNumber,
                //     FirstName = manager.User.FirstName,
                //     LastName = manager.User.LastName,
                //     Email = manager.User.Email,
                //     PhoneNumber = manager.User.PhoneNumber,
                //     ProfilePicture = manager.User.ProfilePicture,
                // }
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
                    Message = "Successful",
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
            if (!managers.Any())
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
                    // ProfilePicture = m.User.ProfilePicture,
                })
            };
        }



        public async Task<BaseResponse<ManagerDto>> UpdateAsync(Guid id, UpdateManagerRequestModel model)
        {
            var manager = await _managerRepository.GetAsync(a => a.Id == id || a.UserId == id);
            if (manager is not null)
            {
                if (model.ProfilePicture != null)
                {
                    var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);
                    manager.User.ProfilePicture = profilePicture.name;
                }

                manager.User.Address = model.Address;
                manager.User.PhoneNumber = model.PhoneNumber;
                manager.User.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);


                // manager.User.FirstName = model.FirstName ?? manager.User.FirstName;
                // manager.User.LastName = model.LastName ?? manager.User.LastName;
                // manager.User.Address = model.Address ?? manager.User.Address;
                // manager.User.PhoneNumber = model.PhoneNumber ?? manager.User.PhoneNumber;
                // manager.User.Email = model.Email ?? manager.User.Email;

                // manager.User.Password = model.Password;

                _managerRepository.Update(manager);
                await _managerRepository.SaveAsync();

                return new BaseResponse<ManagerDto>
                {
                    Message = "Profile Updated Successfully",
                    Status = true,
                    Data = new ManagerDto
                    {
                        ProfilePicture = manager.User.ProfilePicture,
                        PhoneNumber = manager.User.PhoneNumber,
                        Address = manager.User.Address,
                        Password = manager.User.Password,
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
            return "EP/MAG/" + $"{count + 1}";
        }


 // public async Task<BaseResponse<ManagerDto>> UpdateAsync(Guid id, UpdateManagerRequestModel model)
        // {
        //     var user = await _userRepository.GetAsync(a => a.Id == id ) ;
        //     if (user is not null)
        //     {
        //         if (model.ProfilePicture != null)
        //         {
        //             var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);
        //            user.ProfilePicture = profilePicture;
        //         }

        //        user.Address = model.Address;
        //        user.PhoneNumber = model.PhoneNumber;
        //        user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

        //         _userRepository.Update(user);
        //         await _userRepository.SaveAsync();

        //         return new BaseResponse<ManagerDto>
        //         {
        //             Message = "Profile Updated Successfully",
        //             Status = true,
        //             Data = new ManagerDto
        //             {
        //                 ProfilePicture = user.ProfilePicture,
        //                 PhoneNumber = user.PhoneNumber,
        //                 Address = user.Address,
        //                 Password = user.Password,
        //             }
        //         };
        //     }
        //     return new BaseResponse<ManagerDto>
        //     {
        //         Message = "Unable to Update",
        //         Status = false,
        //     };
        // }

       

    }
}

