using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using System.Globalization;
using Microsoft.Extensions.Configuration;
using Application.Authentication;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        //private readonly IJwtAuthenticationManager _tokenService;
        private readonly ITokenService1 _tokenService;
        // private string generatedToken = null;
        private readonly IUserRepository _userRepository;
        private readonly IFarmerRepository _farmerRepository;
        public UserService(IConfiguration config, IUserRepository userRepository, IFarmerRepository farmerRepository, ITokenService1 tokenService)
        {
            _config = config;
            _userRepository = userRepository;
            _farmerRepository = farmerRepository;
            _tokenService = tokenService;
        }
        public async Task<BaseResponse<UserDto>> LoginAsync(LoginUserRequestModel model)
        {
            var user = await _userRepository.GetAsync(a => a.Email == model.Email);

            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {

                var userClaims = new JwtUserTokenClaims
                {
                    UserId = user.Id,
                    Email = user.Email,
                    RoleId = user.Role.Id,
                    RoleName = user.Role.RoleName,
                };

                var userFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";
                var userFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.LastName)}";
                var fullName = userFirstLetterOfFirstNameToUpperCase + " " + userFirstLetterOfLastNameToUpperCase;

                var accessToken = _tokenService.CreateToken(userClaims);


                return new BaseResponse<UserDto>
                {
                    Message = $"{fullName}, Logged in Successful",
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
                        Token = accessToken,
                        //Token = _tokenService.GenerateToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), userDto)

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
                        // Id = g.Id,
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







        // public async Task<BaseResponse<UserDto>> GetUserByTokenAsync(string token)
        // {
        //     var user = await _userRepository.GetAsync(x => x.Token == token);

        //      if (user != null)
        //     {
        //         var userFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";

        //         return new BaseResponse<UserDto>
        //         {
        //             Message = $"{userFirstLetterToUpperCase} found successfully",
        //             Status = true,
        //             Data = new UserDto
        //             {
        //                 Id = user.Id,
        //                 FirstName = user.FirstName,
        //                 LastName = user.LastName,
        //                 Email = user.Email,
        //                 PhoneNumber = user.PhoneNumber,
        //                 RoleId = user.Role.Id,
        //                 RoleName = user.Role.RoleName,
        //                 RoleDescription = user.Role.RoleDescription,
        //                 Address = user.Address,
        //                 Gender = user.Gender,
        //                 // ProfilePicture = user.ProfilePicture,
        //             }
        //         };
        //     }
        //     return new BaseResponse<UserDto>
        //     {
        //         Message = "User not found!",
        //         Status = false,
        //     };
        // }


    }
}
