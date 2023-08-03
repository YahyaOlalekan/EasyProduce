using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface ICartItemForOrderService
    {
        Task<BaseResponse<CartItemForOrderDto>> CreateAsync(CreateCartItemForOrderRequestModel model);
        //BaseResponse<CartItemForOrderDto> Update(Guid id, UpdateOrderCartRequestModel model);
        Task<BaseResponse<CartItemForOrderDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<CartItemForOrderDto>>> GetAllAsync();
        Task<BaseResponse<IEnumerable<CartItemForOrderDto>>> GetAllByUserIdAsync(Guid userId);
        Task<BaseResponse<CartItemForOrderDto>> DeleteAsync(Guid id);
    }
}