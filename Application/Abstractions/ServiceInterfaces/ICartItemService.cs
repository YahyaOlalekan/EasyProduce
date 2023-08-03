using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface ICartItemService
    {
        Task<BaseResponse<CartItemDto>> CreateAsync(CreateCartItemRequestModel model);
        //BaseResponse<CartItemDto> Update(Guid id, UpdateCartItemRequestModel model);
        Task<BaseResponse<CartItemDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<CartItemDto>>> GetAllAsync();
        Task<BaseResponse<IEnumerable<CartItemDto>>> GetAllByUserIdAsync(Guid userId);
        Task<BaseResponse<CartItemDto>> DeleteAsync(Guid id);
    }
}