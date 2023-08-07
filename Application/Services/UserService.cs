using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using System.Globalization;
using Microsoft.Extensions.Configuration;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IJwtAuthenticationManager _tokenService;
        private string generatedToken = null;
        private readonly IUserRepository _userRepository;
        private readonly IFarmerRepository _farmerRepository;
        public UserService(IConfiguration config, IJwtAuthenticationManager tokenService, IUserRepository userRepository, IFarmerRepository farmerRepository)
        {
            _config = config;
            _tokenService = tokenService;
            _userRepository = userRepository;
            _farmerRepository = farmerRepository;
        }
        public async Task<BaseResponse<UserDto>> LoginAsync(LoginUserRequestModel model)
        {
            var user = await _userRepository.GetAsync(a => a.Email == model.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                var userDto = new UserDto
                {
                    Email = user.Email,
                    Id = user.Id,
                    RoleId = user.Role.Id,
                };

                if (user.Role.RoleName.ToLower() == "farmer")
                {

                    var farmer = await _farmerRepository.GetAsync(f => f.UserId == user.Id);
                    var firstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmer.FarmName)}";

                    if (farmer.FarmerRegStatus != Domain.Enum.FarmerRegStatus.Approved)
                    {
                        if (farmer.FarmerRegStatus == Domain.Enum.FarmerRegStatus.Pending)
                        {
                            return new BaseResponse<UserDto>
                            {
                                Message = $"Dear {firstLetterToUpperCase}, approval of your application is still pending!",
                                Status = false,
                            };
                        }
                        else if (farmer.FarmerRegStatus == Domain.Enum.FarmerRegStatus.Declined)
                        {
                            return new BaseResponse<UserDto>
                            {
                                Message = $"Dear {firstLetterToUpperCase}, Sorry, your application is declined!",
                                Status = false,
                            };
                        }
                    }
                }

                var userFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";


                return new BaseResponse<UserDto>
                {
                    Message = $"{userFirstLetterToUpperCase}, Logged in Successful",
                    Status = true,
                    Data = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        RoleId = user.Role.Id,
                        RoleName = user.Role.RoleName,
                        RoleDescription = user.Role.RoleDescription,
                        Address = user.Address,
                        Gender = user.Gender,
                        ProfilePicture = user.ProfilePicture,

                        Token = generatedToken = _tokenService.GenerateToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), userDto)

                    }
                };

            }

            return new BaseResponse<UserDto>
            {
                Message = "Incorrect email or password",
                Status = false
            };
        }

        public async Task<BaseResponse<UserDto>> GetAsync(Guid id)
        {
            var user = await _userRepository.GetAsync(id);
            if (user != null)
            {
                var userFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";

                return new BaseResponse<UserDto>
                {
                    Message = $"{userFirstLetterToUpperCase} found successfully",
                    Status = true,
                    Data = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        RoleId = user.Role.Id,
                        RoleName = user.Role.RoleName,
                        RoleDescription = user.Role.RoleDescription,
                        Address = user.Address,
                        Gender = user.Gender,
                        // ProfilePicture = user.ProfilePicture,
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "User not found!",
                Status = false,
            };
        }

        public async Task<BaseResponse<List<UserDto>>> GetAllAsync()
        {
            var getUsers = await _userRepository.GetAllAsync();
            if (getUsers.Any())
            {
                return new BaseResponse<List<UserDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = getUsers.Select(g => new UserDto
                    {
                        Id = g.Id,
                        FirstName = g.FirstName,
                        LastName = g.LastName,
                        Email = g.Email,
                        PhoneNumber = g.PhoneNumber,
                        RoleId = g.Role.Id,
                        RoleName = g.Role.RoleName,
                        RoleDescription = g.Role.RoleDescription,
                        Address = g.Address,
                        Gender = g.Gender,
                        // ProfilePicture = g.ProfilePicture,

                    }).ToList()
                };
            }
            return new BaseResponse<List<UserDto>>
            {
                Message = "Not Successful",
                Status = false,
            };
        }



        public async Task<BaseResponse<List<UserDto>>> GetAllUsersByRoleAsync(string role)

        {
            var users = await _userRepository.GetSelectedAsync(u => u.Role.RoleName == role.ToLower());
            if (!users.Any())
            {
                return new BaseResponse<List<UserDto>>
                {
                    Message = $"No User found for the role '{role}'",
                    Status = false
                };
            }

            return new BaseResponse<List<UserDto>>
            {
                Message = $"Users with role '{role}' found successfully",
                Status = true,
                Data = users.Select(g => new UserDto
                {
                    Id = g.Id,
                    FirstName = g.FirstName,
                    LastName = g.LastName,
                    Email = g.Email,
                    PhoneNumber = g.PhoneNumber,
                    RoleId = g.Role.Id,
                    RoleName = g.Role.RoleName,
                    RoleDescription = g.Role.RoleDescription,
                    Address = g.Address,
                    Gender = g.Gender,
                    // ProfilePicture = g.ProfilePicture,

                }).ToList()
            };

        }


        public async Task<BaseResponse<UserDto>> GetUserByTokenAsync(string token)
        {
            var user = await _userRepository.GetAsync(x => x.Token == token);

             if (user != null)
            {
                var userFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";

                return new BaseResponse<UserDto>
                {
                    Message = $"{userFirstLetterToUpperCase} found successfully",
                    Status = true,
                    Data = new UserDto
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        RoleId = user.Role.Id,
                        RoleName = user.Role.RoleName,
                        RoleDescription = user.Role.RoleDescription,
                        Address = user.Address,
                        Gender = user.Gender,
                        // ProfilePicture = user.ProfilePicture,
                    }
                };
            }
            return new BaseResponse<UserDto>
            {
                Message = "User not found!",
                Status = false,
            };
        }


    }
}
