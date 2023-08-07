using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> LoginAsync(LoginUserRequestModel model);
        Task<BaseResponse<UserDto>> GetAsync(Guid id);
        Task<BaseResponse<List<UserDto>>> GetAllAsync();
        Task<BaseResponse<List<UserDto>>> GetAllUsersByRoleAsync(string role);
        Task<BaseResponse<UserDto>> GetUserByTokenAsync(string token);

       // Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllAsync(Func<FarmerDto, bool> expression);
       // Task<IEnumerable<UserDto>> GetSelectedAsync(Expression<Func<UserDto, bool>> expression);



    }
}