using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProduceTypeService(IProduceTypeRepository produceTypeRepository, IHttpContextAccessor httpAccessor)
        {
            _produceTypeRepository = produceTypeRepository;
            _httpAccessor = httpAccessor;
        }

        public async Task<BaseResponse<ProduceTypeDto>> CreateAsync(CreateProduceTypeRequestModel model)
        {
            // var loginId = _httpAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var produceTypeExist = await _produceTypeRepository.GetAsync(a => a.TypeName == model.TypeName);
            if (produceTypeExist == null)
            {
                var produceType = new ProduceType();
                produceType.TypeName = model.TypeName;
                produceType.UnitOfMeasurement = model.UnitOfMeasurement;
                produceType.CostPrice = model.CostPrice;
                produceType.SellingPrice = model.SellingPrice;
                produceType.ProduceId = model.ProduceId;
               // produceType.TypePicture = model.TypePicture;
                // produceType.CreatedBy = loginId;

                await _produceTypeRepository.CreateAsync(produceType);
                await _produceTypeRepository.SaveAsync();

                return new BaseResponse<ProduceTypeDto>
                {
                    Message = "Produce Type Successfully Created",
                    Status = true,
                    Data = null,
                  
                    // Data = new ProduceTypeDto
                    // {
                    //     Id = produceType.Id,
                    //     TypeName = produceType.TypeName,
                    //     UnitOfMeasurement = produceType.UnitOfMeasurement,
                    //     CostPrice = produceType.CostPrice,
                    //     SellingPrice = produceType.SellingPrice,

                    // }
                };
            }
            return new BaseResponse<ProduceTypeDto>
            {
                Message = "Produce Type Already Exists!",
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
            return new BaseResponse<ProduceTypeDto>
            {
                Message = "Produce Type Deleted Successfully",
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
            if (produceType.Count() == 0)
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
                produceType.UnitOfMeasurement = model.UnitOfMeasurement;
                produceType.CostPrice = model.CostPrice;
                produceType.SellingPrice = model.SellingPrice;
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
    }
}