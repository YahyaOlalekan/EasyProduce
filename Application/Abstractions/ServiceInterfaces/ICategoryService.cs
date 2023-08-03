using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface ICategoryService
    {
        Task<BaseResponse<CategoryDto>> CreateAsync(CreateCategoryRequestModel model);
        Task<BaseResponse<CategoryDto>> UpdateAsync(Guid id, UpdateCategoryRequestModel model);
        Task<BaseResponse<CategoryDto>> GetAsync(Guid id);
        BaseResponse<IEnumerable<CategoryDto>> GetAllAsync();
        Task<BaseResponse<CategoryDto>> DeleteAsync(Guid id);
    }
}

