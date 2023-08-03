using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> LoginAsync(LoginUserRequestModel model);
        Task<BaseResponse<UserDto>> GetAsync(Guid id);
        Task<BaseResponse<List<UserDto>>> GetAllAsync();

    }
}