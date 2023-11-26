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
        private readonly IProduceTypeRepository _produceTypeRepository;
        private readonly IRequestRepository _requestRepository;

        public RequestService(IFarmerRepository farmerRepository, IFarmerProduceTypeRepository farmerProduceTypeRepository, IProduceTypeRepository produceTypeRepository, IRequestRepository requestRepository)
        {
            _farmerRepository = farmerRepository;
            _farmerProduceTypeRepository = farmerProduceTypeRepository;
            _produceTypeRepository = produceTypeRepository;
            _requestRepository = requestRepository;
        }

        public async Task<BaseResponse<Request>> AddNewProduceTypeAsync(Guid farmerId, Guid produceTypeId)
        {
            var farmer = await _farmerRepository.GetAsync(f => f.User.Id == farmerId);
            if (farmer == null)
            {

                return new BaseResponse<Request>
                {
                    Message = "Farmer Not Found",
                    Status = false
                };
            }

            var checkIfNewRequestExists = await _requestRepository.GetRequestAsync(r => r.FarmerId == farmer.Id && r.ProduceTypeId == produceTypeId && r.RequestType == Domain.Enum.RequestType.AddNewProduceType && r.RequestStatus == Domain.Enum.RequestStatus.Initialized);
            if (checkIfNewRequestExists != null)
            {
                return new BaseResponse<Request>
                {
                    Message = "You have made this request before!",
                    Status = false
                };
            }

            if (farmer.FarmerRegStatus != Domain.Enum.FarmerRegStatus.Approved)
            {
                return new BaseResponse<Request>
                {
                    Message = "Sorry, You are yet to be approved as a produce type Seller",
                    Status = false
                };
            }

            var approvedProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmer.Id && x.ProduceTypeId == produceTypeId && x.Status == Domain.Enum.Status.Approved);
            if (approvedProduceType != null)
            {
                return new BaseResponse<Request>
                {
                    Message = $"Sorry, '{approvedProduceType.ProduceType.TypeName}' producetype has been approved for you before!",
                    Status = false
                };
            }

            var allProduceTypes = await _produceTypeRepository.GetAllAsync();

            var approvedProduceTypes = await _farmerProduceTypeRepository.GetAllAsync(f => f.FarmerId == farmerId && !f.IsDeleted && f.Status == Domain.Enum.Status.Approved);
            var approvedProduceTypesIds = approvedProduceTypes.Select(p => p.ProduceTypeId).ToList();
            var unapprovedProduceTypes = allProduceTypes.Where(pt => !approvedProduceTypesIds.Contains(pt.Id)).ToList();

            if (!unapprovedProduceTypes.Any())
            {
                return new BaseResponse<Request>
                {
                    Message = "Sorry, no produce is available for you to request",
                    Status = false
                };
            }


            var request = new Request
            {
                ProduceTypeId = produceTypeId,
                // RejectionReason = RejectionReason,
                RequestType = Domain.Enum.RequestType.AddNewProduceType,
                RequestStatus = Domain.Enum.RequestStatus.Initialized,
                FarmerId = farmer.Id,
                RequestNumber = await _requestRepository.GenerateRequestNumAsync()

            };

            await _requestRepository.CreateRequestAsync(request);
            await _requestRepository.SaveAsync();

            var farmerFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmer.User.FirstName)}";
            var farmerFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(farmer.User.LastName)}";
            var fullName = farmerFirstLetterOfFirstNameToUpperCase + " " + farmerFirstLetterOfLastNameToUpperCase;

            var produceType = await _produceTypeRepository.GetAsync(produceTypeId);


            return new BaseResponse<Request>
            {
                Message = $"Dear {fullName}, your request to add produce type '{produceType.TypeName}' is successfully submitted",
                Status = true
            };
        }



        public async Task<BaseResponse<Request>> RemoveExistingProduceTypeAsync(Guid farmerId, RemoveExistingProduceTypeRequestModel model)
        {
            var farmer = await _farmerRepository.GetAsync(f => f.User.Id == farmerId);
            if (farmer == null)
            {
                return new BaseResponse<Request>
                {
                    Message = "Farmer Not Found",
                    Status = false
                };
            }

            var checkIfNewRequestExists = await _requestRepository.GetRequestAsync(r => r.FarmerId == farmer.Id && r.ProduceTypeId == model.ProduceTypeId && r.RequestType == Domain.Enum.RequestType.RemoveFromExistingProduceType && r.RequestStatus == Domain.Enum.RequestStatus.Initialized);
            if (checkIfNewRequestExists != null)
            {
                return new BaseResponse<Request>
                {
                    Message = "You have made this request before!",
                    Status = false
                };
            }

            if (farmer.FarmerRegStatus != Domain.Enum.FarmerRegStatus.Approved)
            {
                return new BaseResponse<Request>
                {
                    Message = "Sorry, You are yet to be approved as a produce type Seller",
                    Status = false
                };
            }

            var approvedProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmer.Id && x.ProduceTypeId == model.ProduceTypeId && x.Status == Domain.Enum.Status.Approved);
            if (approvedProduceType == null)
            {
                return new BaseResponse<Request>
                {
                    Message = "Sorry, this produce Type has never been approved for you before!",
                    Status = false
                };
            }

            var approvedProduceTypes = await _farmerProduceTypeRepository.GetAllAsync(x => x.FarmerId == farmer.Id && x.Status == Domain.Enum.Status.Approved);

            if (approvedProduceTypes.Count() == 1)
            {
                return new BaseResponse<Request>
                {
                    Message = "Sorry, you cannot remove your only approved producetype or else you will be deactivated!",
                    Status = false
                };
            }

            var request = new Request
            {
                ProduceTypeId = model.ProduceTypeId,
                RejectionReason = model.ReasonForStopSelling,
                RequestType = Domain.Enum.RequestType.RemoveFromExistingProduceType,
                RequestStatus = Domain.Enum.RequestStatus.Initialized,
                FarmerId = farmer.Id,
                RequestNumber = await _requestRepository.GenerateRequestNumAsync()
            };

            await _requestRepository.CreateRequestAsync(request);
            await _requestRepository.SaveAsync();

            var farmerFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(approvedProduceType.Farmer.User.FirstName)}";
            var farmerFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(approvedProduceType.Farmer.User.LastName)}";
            var fullName = farmerFirstLetterOfFirstNameToUpperCase + " " + farmerFirstLetterOfLastNameToUpperCase;

            return new BaseResponse<Request>
            {
                Message = $"Dear {fullName}, your request to remove produce type '{approvedProduceType.ProduceType.TypeName}' is successfully submitted",
                Status = true
            };

        }



        public async Task<BaseResponse<Request>> VerifyRequestAsync(RequestApproveRequestModel model)
        {
            var request = await _requestRepository.GetRequestAsync(x => x.FarmerId == model.FarmerId && x.Id == model.RequestId);

            if (request == null)
            {
                return new BaseResponse<Request>
                {
                    Message = "Request Not found",
                    Status = false
                };
            }

            if (request.RequestType == Domain.Enum.RequestType.AddNewProduceType)
            {
                if (request.RequestStatus == Domain.Enum.RequestStatus.Approved)
                {
                    var farmerProduceType = (await _farmerProduceTypeRepository.GetSelectedAsync(f => f.FarmerId == request.FarmerId && f.ProduceTypeId == request.ProduceTypeId && !(f.Status == Domain.Enum.Status.Approved))).FirstOrDefault();
                    if (farmerProduceType != null)
                    {
                        farmerProduceType.Status = Domain.Enum.Status.Approved;
                        _farmerProduceTypeRepository.Update(farmerProduceType);
                    }
                }
                else
                {
                    var newProduceType = new FarmerProduceType
                    {
                        FarmerId = request.FarmerId,
                        ProduceTypeId = request.ProduceTypeId,
                        Status = Domain.Enum.Status.Approved,
                    };

                    await _farmerProduceTypeRepository.CreateAsync(newProduceType);

                }




            }
            else
            {
                if (request.RequestStatus == Domain.Enum.RequestStatus.Approved)
                {
                    var farmerProduceType = (await _farmerProduceTypeRepository.GetSelectedAsync(f => f.FarmerId == request.FarmerId && f.ProduceTypeId == request.ProduceTypeId && f.Status == Domain.Enum.Status.Approved)).FirstOrDefault();
                    if (farmerProduceType == null)
                    {
                        return new BaseResponse<Request>
                        {
                            Message = "Producetype Not found",
                            Status = false
                        };
                    }
                    farmerProduceType.Status = Domain.Enum.Status.Deactivated;
                    _farmerProduceTypeRepository.Update(farmerProduceType);


                }
                
                // if (request.RequestStatus == Domain.Enum.RequestStatus.Approved)
                // {
                //     var farmerProduceType = (await _farmerProduceTypeRepository.GetSelectedAsync(f => f.FarmerId == request.FarmerId && f.ProduceTypeId == request.ProduceTypeId && f.Status == Domain.Enum.Status.Approved)).FirstOrDefault();
                //     if (farmerProduceType == null)
                //     {
                //         return new BaseResponse<Request>
                //         {
                //             Message = "Producetype Not found",
                //             Status = false
                //         };
                //     }
                //     farmerProduceType.Status = Domain.Enum.Status.Deactivated;
                //     _farmerProduceTypeRepository.Update(farmerProduceType);


                // }
            }

            request.RejectionReason = model.RejectionReason;
            request.RequestStatus = model.RequestStatus;
            _requestRepository.Update(request);
            await _requestRepository.SaveAsync();

            return new BaseResponse<Request>
            {
                Message = "Successful",
                Status = true
            };
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
                    Id = c.Id,
                    RequestType = c.RequestType,
                    RequestStatus = c.RequestStatus,
                    ReasonForStopSelling = c?.ReasonForStopSelling,
                    RejectionReason = c?.RejectionReason,
                    FarmerId = c.Farmer.Id,
                    ProduceTypeId = c.ProduceTypeId,
                    RegistrationNumber = c.Farmer.RegistrationNumber,
                    TypeName = c.ProduceType?.TypeName

                    // Email = c.Farmer?.User?.Email,
                    //RequestNumber =c.RequestNumber,

                })
            };
        }


        // public async Task<string> GetProduceTypeNameByIdAsync(Guid produceTypeId)
        // {
        //     var produceType = await _produceTypeRepository.GetAsync(pt => pt.Id == produceTypeId);
        //     return produceType?.TypeName;
        // }






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