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
    public class ProduceService : IProduceService
    {
        private readonly IProduceRepository _produceRepository;
        private readonly IHttpContextAccessor _httpAccessor;

        public ProduceService(IProduceRepository produceRepository, IHttpContextAccessor httpAccessor)
        {
            _produceRepository = produceRepository;
            _httpAccessor = httpAccessor;
        }

        public async Task<BaseResponse<ProduceDto>> CreateAsync(CreateProduceRequestModel model)
        {
            // var loginId = _httpAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var produceExist = await _produceRepository.GetAsync(a => a.ProduceName == model.ProduceName);
            if (produceExist == null)
            {
                var produce = new Produce();
                produce.ProduceName = model.ProduceName;
                produce.DescriptionOfProduce = model.DescriptionOfProduce;
                produce.CategoryId = model.CategoryId;
                // produce.CreatedBy = loginId;

                await _produceRepository.CreateAsync(produce);
                await _produceRepository.SaveAsync();

                return new BaseResponse<ProduceDto>
                {
                    Message = "Produce Successfully Created",
                    Status = true,
                    Data = new ProduceDto
                    {
                        Id = produce.Id,
                        ProduceName = produce.ProduceName,
                        DescriptionOfProduce = produce.DescriptionOfProduce
                    }
                };
            }
            return new BaseResponse<ProduceDto>
            {
                Message = "Produce Already Exists!",
                Status = false
            };

        }



        public async Task<BaseResponse<ProduceDto>> DeleteAsync(Guid id)
        {
            var produce = await _produceRepository.GetAsync(id);
            if (produce is null)
            {
                return new BaseResponse<ProduceDto>
                {
                    Message = "The produce does not exist",
                    Status = false
                };
            }
            produce.IsDeleted = true;

            _produceRepository.Update(produce);
            await _produceRepository.SaveAsync();
            return new BaseResponse<ProduceDto>
            {
                Message = "Produce Deleted Successfully",
                Status = true
            };

        }


        public async Task<BaseResponse<ProduceDto>> GetAsync(Guid id)
        {
            var produce = await _produceRepository.GetAsync(id);
            if (produce == null)
            {
                return new BaseResponse<ProduceDto>
                {
                    Message = "The Produce is Not found!",
                    Status = false,
                };
            }
            return new BaseResponse<ProduceDto>
            {
                Message = "Found",
                Status = true,
                Data = new ProduceDto
                {
                    Id = produce.Id,
                    ProduceName = produce.ProduceName,
                    DescriptionOfProduce = produce.DescriptionOfProduce
                }
            };
        }



        public async Task<BaseResponse<IEnumerable<ProduceDto>>> GetAllAsync()
        {
            var produce = await _produceRepository.GetAllAsync();
            if (produce.Count() == 0)
            {
                return new BaseResponse<IEnumerable<ProduceDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<ProduceDto>>
            {
                Message = "Found",
                Status = true,
                Data = produce.Select(c => new ProduceDto
                {
                    Id = c.Id,
                   ProduceName = c.ProduceName,
                    DescriptionOfProduce = c.DescriptionOfProduce
                })
            };
        }



        public async Task<BaseResponse<ProduceDto>> UpdateAsync(Guid id, UpdateProduceRequestModel model)
        {
            var produce = await _produceRepository.GetAsync(a => a.Id == id);
            if (produce is not null)
            {

                produce.ProduceName = model.ProduceName;
                produce.DescriptionOfProduce = model.DescriptionOfProduce;

                _produceRepository.Update(produce);
                await _produceRepository.SaveAsync();

                return new BaseResponse<ProduceDto>
                {
                    Message = "Produce Updated Successfully",
                    Status = true,
                    Data = new ProduceDto
                    {
                        ProduceName = produce.ProduceName,
                        DescriptionOfProduce = produce.DescriptionOfProduce,
                        Id = produce.Id,
                    }
                };
            }
            return new BaseResponse<ProduceDto>
            {
                Message = "Unable to Update!",
                Status = false,
            };
        }

       
    }
}