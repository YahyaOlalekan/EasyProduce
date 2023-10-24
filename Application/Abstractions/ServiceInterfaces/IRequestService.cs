using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entity;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface IRequestService
    {
        // Task<string> AddNewProduceTypeAsync(Guid farmerId, AddNewProduceTypeRequestModel model);
        Task<BaseResponse<Request>> AddNewProduceTypeAsync(Guid farmerId, Guid produceTypeId);

        Task<BaseResponse<Request>> RemoveExistingProduceTypeAsync(Guid farmerId, RemoveExistingProduceTypeRequestModel model);
        Task<BaseResponse<Request>> VerifyRequestAsync(RequestApproveRequestModel model);
        Task<BaseResponse<IEnumerable<RequestDto>>> GetAllProduceTypeRequestAsync(GetAllProduceTypeRequestModel model);
    }
}