using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;

namespace Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IFarmerProduceTypeRepository _farmerProduceTypeRepository;
        private readonly IRequestRepository _requestRepository;

        public RequestService(IFarmerRepository farmerRepository, IFarmerProduceTypeRepository farmerProduceTypeRepository, IRequestRepository requestRepository)
        {
            _farmerRepository = farmerRepository;
            _farmerProduceTypeRepository = farmerProduceTypeRepository;
            _requestRepository = requestRepository;
        }

        public async Task<string> AddNewProduceTypeAsync(Guid farmerId, AddNewProduceTypeRequestModel model)
        {
            var farmer = await _farmerRepository.GetAsync(f => f.Id == farmerId);
            if (farmer == null)
            {
                return "Farmer Not Found!";
            }

            if (farmer.FarmerRegStatus != Domain.Enum.FarmerRegStatus.Approved)
            {
                return "Sorry, You are yet to be approved as a produce type Seller";
            }

            var farmerProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmerId && x.ProduceTypeId == model.ProduceTypeId && x.Status == Domain.Enum.Status.Approved);
            if (farmerProduceType != null)
            {
                return $"Sorry, '{farmerProduceType.ProduceType.TypeName}' produce Type has been approved for you before!";
            }

            var request = new Request
            {
                ProduceTypeId = model.ProduceTypeId,
                RejectionReason = model.RejectionReason,
                RequestType = Domain.Enum.RequestType.AddNewProduceType,
                RequestStatus = Domain.Enum.RequestStatus.Initialized,
                FarmerId = farmerId,
                RequestNumber = await _requestRepository.GenerateRequestNumAsync()
                
            };

            await _requestRepository.CreateRequestAsync(request);
            await _requestRepository.SaveAsync();

            var farmerFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmerProduceType.Farmer.User.FirstName)}";
            var farmerFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmerProduceType.Farmer.User.LastName)}";
            var fullName = farmerFirstLetterOfFirstNameToUpperCase + " " + farmerFirstLetterOfLastNameToUpperCase;

            return $"Dear {fullName}, your request to add produce type '{farmerProduceType.ProduceType.TypeName}' is successfully submitted";
        }


        public async Task<string> RemoveExistingProduceTypeAsync(Guid farmerId, RemoveExistingProduceTypeRequestModel model)
        {
            var farmer = await _farmerRepository.GetAsync(f => f.Id == farmerId);
            if (farmer == null)
            {
                return "Farmer Not Found!";
            }

            if (farmer.FarmerRegStatus != Domain.Enum.FarmerRegStatus.Approved)
            {
                return "Sorry, You are yet to be approved as a produce type Seller";
            }

            var farmerProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmerId && x.ProduceTypeId == model.ProduceTypeId && x.Status != Domain.Enum.Status.Approved);
            if (farmerProduceType == null)
            {
                return "Sorry, this produce Type has never been approved for you before!";
            }

            var request = new Request
            {
                ProduceTypeId = model.ProduceTypeId,
                RejectionReason = model.ReasonForStopSelling,
                RequestType = Domain.Enum.RequestType.RemoveFromExistingProduceType,
                RequestStatus = Domain.Enum.RequestStatus.Initialized,
                FarmerId = farmerId,
                RequestNumber = await _requestRepository.GenerateRequestNumAsync()
            };

            await _requestRepository.CreateRequestAsync(request);
            await _requestRepository.SaveAsync();

            var farmerFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmerProduceType.Farmer.User.FirstName)}";
            var farmerFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmerProduceType.Farmer.User.LastName)}";
            var fullName = farmerFirstLetterOfFirstNameToUpperCase + " " + farmerFirstLetterOfLastNameToUpperCase;

            return $"Dear {fullName}, your request to remove produce type '{farmerProduceType.ProduceType.TypeName}' is successfully submitted";

        }


    
        public async Task<string> VerifyRequestAsync(RequestApproveRequestModel model)
        {
            var request = await _requestRepository.GetRequestAsync(x => x.FarmerId == model.FarmerId && x.Id == model.RequestId);

            if (request == null)
            {
                return "Request Not found";
            }

            request.RejectionReason = model.RejectionReason;
            request.RequestStatus = model.RequestStatus;
            _requestRepository.Update(request);
            await _requestRepository.SaveAsync();

            return "Successful";
        }



       
        public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllProduceTypeRequestAsync(GetAllProduceTypeRequestModel model)
        {
            var request = await _requestRepository.GetSelectedAsync(r => r.RequestType == model.RequestType && (model.RequestStatus == null || r.RequestStatus == model.RequestStatus));
            if (!request.Any())
            {
                return new BaseResponse<IEnumerable<RequestDto>>
                {
                    Message = "Not Found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<RequestDto>>
            {
                Message = "Found",
                Status = true,
                Data = request.Select(c => new RequestDto
                {
                    RequestType = c.RequestType,
                    RequestStatus = c.RequestStatus,
                    ReasonForStopSelling = c.ReasonForStopSelling,
                    RejectionReason = c.RejectionReason,
                    Email = c.Farmer.User.Email,

                })
            };
        }
       
       
       
       
       
        // public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllRequestsAsync()
        // {
        //     var request = await _requestRepository.GetAllAsync();
        //     if (!request.Any())
        //     {
        //         return new BaseResponse<IEnumerable<RequestDto>>
        //         {
        //             Message = "Not found",
        //             Status = false,
        //         };
        //     }
        //     return new BaseResponse<IEnumerable<RequestDto>>
        //     {
        //         Message = "Found",
        //         Status = true,
        //         Data = request.Select(c => new RequestDto
        //         {
        //             RequestType = c.RequestType,
        //             RequestStatus = c.RequestStatus,
        //             ReasonForStopSelling = c.ReasonForStopSelling,
        //             RejectionReason = c.RejectionReason,
        //             // Email = c.Farmer.User.Email,

        //         })
        //     };
        // }
        // public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllAddNewProduceTypeRequestAsync()
        // {
        //     var request = await _requestRepository.GetSelectedAsync(r => r.RequestType == RequestType.AddNewProduceType);
        //     if (!request.Any())
        //     {
        //         return new BaseResponse<IEnumerable<RequestDto>>
        //         {
        //             Message = "Not found",
        //             Status = false,
        //         };
        //     }
        //     return new BaseResponse<IEnumerable<RequestDto>>
        //     {
        //         Message = "Found",
        //         Status = true,
        //         Data = request.Select(c => new RequestDto
        //         {
        //             RequestType = c.RequestType,
        //             RequestStatus = c.RequestStatus,
        //             ReasonForStopSelling = c.ReasonForStopSelling,
        //             RejectionReason = c.RejectionReason,
        //             // Email = c.Farmer.User.Email,

        //         })
        //     };
        // }
       
        // public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllRemoveProduceTypeRequestAsync()
        // {
        //     var request = await _requestRepository.GetSelectedAsync(r => r.RequestType == RequestType.RemoveFromExistingProduceType);
        //     if (!request.Any())
        //     {
        //         return new BaseResponse<IEnumerable<RequestDto>>
        //         {
        //             Message = "Not found",
        //             Status = false,
        //         };
        //     }
        //     return new BaseResponse<IEnumerable<RequestDto>>
        //     {
        //         Message = "Found",
        //         Status = true,
        //         Data = request.Select(c => new RequestDto
        //         {
        //             RequestType = c.RequestType,
        //             RequestStatus = c.RequestStatus,
        //             ReasonForStopSelling = c.ReasonForStopSelling,
        //             RejectionReason = c.RejectionReason,
        //             // Email = c.Farmer.User.Email,

        //         })
        //     };
        // }
        // public async Task<BaseResponse<IEnumerable<RequestDto>>> GetAllApprovedRemoveProduceTypeRequestAsync()
        // {
        //     var request = await _requestRepository.GetSelectedAsync(r => r.RequestType == RequestType.RemoveFromExistingProduceType && r.RequestStatus == RequestStatus.Approved);  
        //     if (!request.Any())
        //     {
        //         return new BaseResponse<IEnumerable<RequestDto>>
        //         {
        //             Message = "Not found",
        //             Status = false,
        //         };
        //     }
        //     return new BaseResponse<IEnumerable<RequestDto>>
        //     {
        //         Message = "Found",
        //         Status = true,
        //         Data = request.Select(c => new RequestDto
        //         {
        //             RequestType = c.RequestType,
        //             RequestStatus = c.RequestStatus,
        //             ReasonForStopSelling = c.ReasonForStopSelling,
        //             RejectionReason = c.RejectionReason,
        //             // Email = c.Farmer.User.Email,

        //         })
        //     };
        // }












    }
}