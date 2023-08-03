using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IManagerService
    {
        Task<BaseResponse<ManagerDto>> CreateAsync(Guid loginId, CreateManagerRequestModel model);
        // BaseResponse<ManagerDto> Create(Guid id, CreateManagerRequestModel model);
        Task<BaseResponse<ManagerDto>> UpdateAsync(Guid id, UpdateManagerRequestModel model);
        Task<BaseResponse<ManagerDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<ManagerDto>>> GetAllAsync();
        Task<BaseResponse<ManagerDto>> DeleteAsync(Guid id);
        //Task<BaseResponse<ManagerDto>>  GetByEmail(Guid email);
     
    }
}