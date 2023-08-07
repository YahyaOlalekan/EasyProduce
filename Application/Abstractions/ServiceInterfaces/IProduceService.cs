using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IProduceService
    {
        Task<BaseResponse<ProduceDto>> CreateAsync(CreateProduceRequestModel model);
        Task<BaseResponse<ProduceDto>> UpdateAsync(Guid id, UpdateProduceRequestModel model);
        Task<BaseResponse<ProduceDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<ProduceDto>>> GetAllAsync();
        Task<BaseResponse<ProduceDto>> DeleteAsync(Guid id);
    }
}

