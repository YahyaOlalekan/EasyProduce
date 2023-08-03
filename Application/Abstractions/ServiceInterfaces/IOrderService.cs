using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IOrderService
    {
        Task<BaseResponse<OrderDto>> CreateAsync(CreateOrderRequestModel model);
        Task<BaseResponse<OrderDto>> UpdateAsync(Guid id, UpdateOrderRequestModel model);
        Task<BaseResponse<OrderDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllAsync();
        Task<BaseResponse<OrderDto>> DeleteAsync(Guid id);
        Task<BaseResponse<OrderDto>> DetailsAsync(Guid OrderNumber);
        Task<BaseResponse<IEnumerable<OrderDto>>> GetAllByUserIdAsync(Guid userId);
    }
}