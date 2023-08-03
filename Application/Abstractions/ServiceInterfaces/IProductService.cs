using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IProductService
    {
        Task<BaseResponse<ProductDto>> CreateAsync(CreateProductRequestModel model);
        Task<BaseResponse<ProductDto>> UpdateAsync(Guid id, UpdateProductRequestModel model);
        Task<BaseResponse<ProductDto>> GetAsync(Guid id);
        BaseResponse<IEnumerable<ProductDto>> GetAllAsync();
        Task<BaseResponse<ProductDto>> DeleteAsync(Guid id);
    }
}

