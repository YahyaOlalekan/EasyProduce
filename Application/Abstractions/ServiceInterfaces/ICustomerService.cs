
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface ICustomerService
    {
        Task<BaseResponse<CustomerDto>> CreateAsync(CreateCustomerRequestModel model);
        Task<BaseResponse<CustomerDto>> UpdateAsync(Guid id, UpdateCustomerRequestModel model);
        Task<BaseResponse<CustomerDto>> GetAsync(Guid id);
        //BaseResponse<IEnumerable<CustomerDto>> GetAll(Func<CustomerDto, bool> expression);
        Task<BaseResponse<IEnumerable<CustomerDto>>> GetAllAsync();
        Task<BaseResponse<CustomerDto>> DeleteAsync(Guid id);

    }
}
