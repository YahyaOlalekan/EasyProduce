using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IRequestService
    {
        Task<string> AddNewProduceTypeAsync(Guid farmerId, AddNewProduceTypeRequestModel model);
        Task<string> RemoveExistingProduceTypeAsync(Guid farmerId, RemoveExistingProduceTypeRequestModel model);
        Task<string> VerifyRequestAsync(RequestApproveRequestModel model);
        Task<BaseResponse<IEnumerable<RequestDto>>> GetAllProduceTypeRequestAsync(GetAllProduceTypeRequestModel model);
    }
}