using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IFarmerService
    {
        Task<BaseResponse<FarmerDto>> CreateAsync(CreateFarmerRequestModel model);
        Task<BaseResponse<FarmerDto>> UpdateAsync(Guid id, UpdateFarmerRequestModel model);
        Task<BaseResponse<FarmerDto>> GetAsync(Guid id);
        // BaseResponse<IEnumerable<FarmerDto>> GetAll(Func<FarmerDto, bool> expression);
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllAsync();
        Task<BaseResponse<FarmerDto>> DeleteAsync(Guid id);
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetPendingFarmersAsync();
        Task<BaseResponse<IEnumerable<FarmerDto>>> ApprovedFarmersAsync();
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetDeclinedFarmersAsync();
        Task<BaseResponse<FarmerDto>> VerifyFarmersAsync(ApproveFarmerDto model);

    }
}