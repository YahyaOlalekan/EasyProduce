using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface ICartService
    {
        Task<BaseResponse<CartDto>> CreateAsync(CreateCartRequestModel model);
        //BaseResponse<CartItemDto> Update(Guid id, UpdateCartItemRequestModel model);
        Task<BaseResponse<CartDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<CartDto>>> GetAllAsync();
        Task<BaseResponse<IEnumerable<CartDto>>> GetAllByUserIdAsync(Guid userId);
        Task<BaseResponse<CartDto>> DeleteAsync(Guid id);
    }
}