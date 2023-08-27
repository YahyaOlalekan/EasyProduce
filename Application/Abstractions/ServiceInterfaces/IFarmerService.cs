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
        Task<BaseResponse<FarmerProduceTypeDto>> GetFarmerAlongWithRegisteredProduceTypeAsync(Guid id);
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllAsync(Func<FarmerDto, bool> expression);
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllFarmersAsync();
        Task<BaseResponse<FarmerDto>> DeleteFarmerAsync(Guid id);
        Task<BaseResponse<IEnumerable<FarmerDto>>> GetFarmersByStatusAsync(FarmerStatusRequestModel model);
        Task<BaseResponse<string>> VerifyFarmerAsync(ApproveFarmerDto model);
        Task<BaseResponse<FarmerDto>> GetFarmerAcountDetailsByIdAsync(Guid id);

    }
}