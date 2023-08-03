using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleDto>> CreateAsync(CreateRoleRequestModel model);
        Task<BaseResponse<RoleDto>> UpdateAsync(Guid id, UpdateRoleRequestModel model);
        Task<BaseResponse<RoleDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<RoleDto>>> GetAllAsync();
        Task<BaseResponse<RoleDto>> DeleteAsync(Guid id);
    }
}