using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entity;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IProduceTypeService
    {
        Task<BaseResponse<ProduceTypeDto>> CreateAsync(CreateProduceTypeRequestModel model);
        Task<BaseResponse<ProduceTypeDto>> PurchaseAsync(Guid id, PurchaseProduceTypeRequestModel model);
        Task<BaseResponse<ProduceTypeDto>> UpdateAsync(Guid id, UpdateProduceTypeRequestModel model);
        Task<BaseResponse<ProduceTypeDto>> GetAsync(Guid id);
        //Task<BaseResponse<ProduceTypeDto>> GetByStatus(Status status);
        Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetAllAsync();
        Task<BaseResponse<ProduceTypeDto>> VerifyProduceTypeAsync(ProduceTypeToBeApprovedRequestModel model);
        // Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetProduceTypesToBeApprovedAsync (ProduceTypeToBeApprovedRequestModel model);
         Task<BaseResponse<IEnumerable<ProduceTypeDto>>>GetProduceTypesToBeApprovedAsync(Guid farmerId);


        Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetByProduceIdAsync(Guid ProduceId);
        Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetByCategoryIdAsync(Guid categoryId);
        Task<BaseResponse<ProduceTypeDto>> DeleteAsync(Guid id);
            //  Task<IEnumerable<ProduceType>> GetAllApprovedProduceTypeOfAFarmer(Guid farmerId);

        Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetApprovedProduceTypesForAFarmerByFarmerIdAsync(Guid farmerId);
        Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetAllApprovedProducetypesOfAFarmerAsync(Guid farmerId);
        Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetAllUnapprovedProducetypesOfAFarmerAsync(Guid farmerId);



    }
}