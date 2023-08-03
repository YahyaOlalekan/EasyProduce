using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IProductTypeService
    {
        Task<BaseResponse<ProductTypeDto>> CreateAsync(CreateProductTypeRequestModel model);
        Task<BaseResponse<ProductTypeDto>> SellAsync(SellProductTypeRequestModel model);
        Task<BaseResponse<ProductTypeDto>> UpdateAsync(Guid id, UpdateProductTypeRequestModel model);
        Task<BaseResponse<ProductTypeDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<ProductTypeDto>>> GetAllAsync();
        Task<BaseResponse<IEnumerable<ProductTypeDto>>> GetByProduceIdAsync(Guid produceId);
        Task<BaseResponse<IEnumerable<ProductTypeDto>>> GetByCategoryIdAsync(Guid categoryId);
        Task<BaseResponse<ProductTypeDto>> DeleteAsync(Guid id);
    }
}