using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ProduceTypeService : IProduceTypeService
    {
        private readonly IProduceTypeRepository _produceTypeRepository;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IFarmerRepository _farmerRepository;
        private readonly IFarmerProduceTypeRepository _farmerProduceTypeRepository;

        public ProduceTypeService(IProduceTypeRepository produceTypeRepository, IHttpContextAccessor httpAccessor, IFarmerRepository farmerRepository, IFarmerProduceTypeRepository farmerProduceTypeRepository)
        {
            _produceTypeRepository = produceTypeRepository;
            _httpAccessor = httpAccessor;
            _farmerRepository = farmerRepository;
            _farmerProduceTypeRepository = farmerProduceTypeRepository;
        }

        public async Task<BaseResponse<ProduceTypeDto>> CreateAsync(CreateProduceTypeRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var produceTypeExist = await _produceTypeRepository.GetAsync(a => a.TypeName.ToLower() == model.TypeName.ToLower());
            if (produceTypeExist == null)
            {
                var produceType = new ProduceType();
                produceType.TypeName = model.TypeName;
                // produceType.UnitOfMeasurement = model.UnitOfMeasurement;
                // produceType.CostPrice = model.CostPrice;
                // produceType.SellingPrice = model.SellingPrice;
                produceType.ProduceId = model.ProduceId;
                produceType.CreatedBy = loginId;
                // produceType.TypePicture = model.TypePicture;

                string produceTypeFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(produceType.TypeName)}";


                await _produceTypeRepository.CreateAsync(produceType);
                await _produceTypeRepository.SaveAsync();

                return new BaseResponse<ProduceTypeDto>
                {
                    Message = $"Produce Type '{produceTypeFirstLetterToUpperCase}' Successfully Created",
                    Status = true,
                    Data = null,
                };
            }

            string produceTypeExistFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(produceTypeExist.TypeName)}";

            return new BaseResponse<ProduceTypeDto>
            {
                Message = $"Produce Type '{produceTypeExistFirstLetterToUpperCase}' Already Exists!",
                Status = false
            };

        }



        public async Task<BaseResponse<ProduceTypeDto>> DeleteAsync(Guid id)
        {
            var produceType = await _produceTypeRepository.GetAsync(id);
            if (produceType is null)
            {
                return new BaseResponse<ProduceTypeDto>
                {
                    Message = "The produce Type does not exist",
                    Status = false
                };
            }
            produceType.IsDeleted = true;

            _produceTypeRepository.Update(produceType);
            await _produceTypeRepository.SaveAsync();

            string produceTypeFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(produceType.TypeName)}";


            return new BaseResponse<ProduceTypeDto>
            {
                Message = $"ProduceType '{produceTypeFirstLetterToUpperCase}' Deleted Successfully",
                Status = true
            };

        }


       



        public async Task<BaseResponse<ProduceTypeDto>> GetAsync(Guid id)
        {
            var produceType = await _produceTypeRepository.GetAsync(id);
            if (produceType == null)
            {
                return new BaseResponse<ProduceTypeDto>
                {
                    Message = "The Produce Type is Not found!",
                    Status = false,
                };
            }
            return new BaseResponse<ProduceTypeDto>
            {
                Message = "Found",
                Status = true,
                Data = new ProduceTypeDto
                {
                    Id = produceType.Id,
                    TypeName = produceType.TypeName,
                    UnitOfMeasurement = produceType.UnitOfMeasurement,
                    CostPrice = produceType.CostPrice,
                    SellingPrice = produceType.SellingPrice,

                }
            };
        }



        public async Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetAllAsync()
        {
            var produceType = await _produceTypeRepository.GetAllAsync();
            if (!produceType.Any())
            {
                return new BaseResponse<IEnumerable<ProduceTypeDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<ProduceTypeDto>>
            {
                Message = "Found",
                Status = true,
                Data = produceType.Select(c => new ProduceTypeDto
                {
                     Id = c.Id,
                    TypeName = c.TypeName,
                    UnitOfMeasurement = c.UnitOfMeasurement,
                    CostPrice = c.CostPrice,
                    SellingPrice = c.SellingPrice,

                })
            };
        }



        public async Task<BaseResponse<ProduceTypeDto>> UpdateAsync(Guid id, UpdateProduceTypeRequestModel model)
        {
            var produceType = await _produceTypeRepository.GetAsync(a => a.Id == id);
            if (produceType is not null)
            {

                produceType.TypeName = model.TypeName;
                // produceType.UnitOfMeasurement = model.UnitOfMeasurement;
                // produceType.CostPrice = model.CostPrice;
                // produceType.SellingPrice = model.SellingPrice;
                // produceType.TypePicture = model.TypePicture;

                _produceTypeRepository.Update(produceType);
                await _produceTypeRepository.SaveAsync();

                return new BaseResponse<ProduceTypeDto>
                {
                    Message = "Produce Type Updated Successfully",
                    Status = true,
                    Data = new ProduceTypeDto
                    {
                        Id = produceType.Id,
                        TypeName = produceType.TypeName,
                        UnitOfMeasurement = produceType.UnitOfMeasurement,
                        CostPrice = produceType.CostPrice,
                        SellingPrice = produceType.SellingPrice,

                    }
                };
            }
            return new BaseResponse<ProduceTypeDto>
            {
                Message = "Unable to Update!",
                Status = false,
            };
        }


        public async Task<BaseResponse<ProduceTypeDto>> VerifyProduceTypeAsync(ProduceTypeToBeApprovedRequestModel model)
        {
            var produceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == model.FarmerId && x.Id == model.ProduceTypeId);

            if (produceType == null)
            {
                return new BaseResponse<ProduceTypeDto>
                {
                    Message = "Not Successful",
                    Status = false,
                };
            }

            produceType.Status = model.Status;
            _farmerProduceTypeRepository.Update(produceType);
            await _farmerProduceTypeRepository.SaveAsync();
            return new BaseResponse<ProduceTypeDto>
            {
                Message = "Successful",
                Status = true,
            };

        }
        public async Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetApprovedProduceTypesForAFarmerAsync(Guid farmerId)
        {
            var approvedProduceType = await _farmerProduceTypeRepository.GetAllApprovedProduceTypeOfAFarmer(farmerId);

            if (!approvedProduceType.Any())
            {

                return new BaseResponse<IEnumerable<ProduceTypeDto>>
                {
                    Message = "Nothing Yet Approved",
                    Status = false,
                };
            }

            return new BaseResponse<IEnumerable<ProduceTypeDto>>
            {
                Message = "Successful",
                Status = true,
                Data = approvedProduceType.Select(x => new ProduceTypeDto
                {
                    TypeName = x.TypeName,
                })
            };

        }



        public Task<BaseResponse<ProduceTypeDto>> PurchaseAsync(Guid id, PurchaseProduceTypeRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetByProduceIdAsync(Guid ProduceId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetByCategoryIdAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<List<ProduceTypeDto>>> GetAllAsync(Func<ProduceTypeDto, bool> expression)
        {
            throw new NotImplementedException();
        }









        //  public async Task<BaseResponse<ProduceTypeDto>> VerifyProduceTypeAsync(ApprovedProduceTypeRequestModel model)
        // {
        //     var farmer = await _farmerRepository.GetAsync(model.FarmerId);
        //     var produceTypes = model.ApprovedProduceTypeModels
        //         .Select(x => new ApprovedProduceTypeModel
        //         {
        //             ProduceTypeId = x.ProduceTypeId,
        //             Status = x.Status
        //         }).ToList();

        //     if (produceTypes != null)
        //     {
        //         foreach (var produceType in produceTypes)
        //         {
        //             var availableProduceType = await _produceTypeRepository.GetAsync(a => a.Id == produceType.ProduceTypeId);

        //             if (availableProduceType == null || farmer == null)
        //             {
        //                 return new BaseResponse<ProduceTypeDto>
        //                 {
        //                     Message = "Farmer not found or Produce Type does not exist",
        //                     Status = false,
        //                 };
        //             }
        //             availableProduceType.Status = produceType.Status;
        //             _produceTypeRepository.Update(availableProduceType);
        //             await _produceTypeRepository.SaveAsync();

        //             return new BaseResponse<ProduceTypeDto>
        //             {
        //                 Message = "Successful",
        //                 Status = true,
        //                 Data = new ProduceTypeDto
        //                 {
        //                     Id = produceType.ProduceTypeId,
        //                     Status = produceType.Status
        //                 }
        //             };
        //         }
        //     }
        //     return new BaseResponse<ProduceTypeDto>
        //     {
        //         Message = " Not Successful",
        //         Status = false,
        //     };
        // }





    }
}