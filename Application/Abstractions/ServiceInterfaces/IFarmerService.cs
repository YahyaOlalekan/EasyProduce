using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IFarmerService
    {
        Task<BaseResponse<FarmerDto>> RegisterFarmerAsync(CreateFarmerRequestModel model);
        Task<BaseResponse<FarmerDto>> UpdateFarmerAsync(Guid id, UpdateFarmerRequestModel model);
        // Task<BaseResponse<FarmerDto>> EditApprovedProduceTypeAsync(Guid id, UpdateFarmerRequestModel model);
        // Task<BaseResponse<FarmerDto>> GetAsync(Guid id);
        public Task<BaseResponse<FarmerProduceTypeDto>> GetFarmerAlongWithRegisteredProduceTypeAsync(Guid id);
        // public Task<BaseResponse<FarmerProduceTypeDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllAsync(Func<FarmerDto, bool> expression);
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllFarmersAsync();
        Task<BaseResponse<FarmerDto>> DeleteFarmerAsync(Guid id);
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetPendingFarmersAsync();
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetApprovedFarmersAsync();
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetDeclinedFarmersAsync();
        Task<BaseResponse<string>> VerifyFarmerAsync(ApproveFarmerDto model);

    }
}