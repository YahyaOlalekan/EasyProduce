using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;
using Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class ProduceTypeService : IProduceTypeService
    {
        private readonly IProduceTypeRepository _produceTypeRepository;
        private readonly IHttpContextAccessor _httpAccessor;
        private readonly IFarmerRepository _farmerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFarmerProduceTypeRepository _farmerProduceTypeRepository;
        private readonly IFileUploadServiceForWWWRoot _fileUploadServiceForWWWRoot;

        public ProduceTypeService(IProduceTypeRepository produceTypeRepository, IHttpContextAccessor httpAccessor, IFarmerRepository farmerRepository, IFarmerProduceTypeRepository farmerProduceTypeRepository, IUserRepository userRepository, IFileUploadServiceForWWWRoot fileUploadServiceForWWWRoot)
        {
            _produceTypeRepository = produceTypeRepository;
            _httpAccessor = httpAccessor;
            _farmerRepository = farmerRepository;
            _userRepository = userRepository;
            _farmerProduceTypeRepository = farmerProduceTypeRepository;
            _fileUploadServiceForWWWRoot = fileUploadServiceForWWWRoot;
        }

        public async Task<BaseResponse<ProduceTypeDto>> CreateAsync(CreateProduceTypeRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var produceTypeExist = await _produceTypeRepository.GetAsync(a => a.TypeName.ToLower() == model.TypeName.ToLower());
            if (produceTypeExist == null)
            {
                var produceType = new ProduceType();
                produceType.TypeName = model.TypeName;
                produceType.ProduceId = model.ProduceId;
                produceType.CreatedBy = loginId;

                // produceType.UnitOfMeasurement = model.UnitOfMeasurement;
                // produceType.CostPrice = model.CostPrice;
                // produceType.SellingPrice = model.SellingPrice;

                var typePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(
               model.TypePicture
           );
                 produceType.TypePicture = typePicture.name;

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
                   // SellingPrice = produceType.SellingPrice,

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
                    // UnitOfMeasurement = c.UnitOfMeasurement,
                    // CostPrice = c.CostPrice,
                    // SellingPrice = c.SellingPrice,

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
                        //SellingPrice = produceType.SellingPrice,

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
            var produceType = await _farmerProduceTypeRepository.GetAsync(model.FarmerId, model.ProduceTypeId);
            // var produceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == model.FarmerId && x.Farmer.FarmerRegStatus == Domain.Enum.FarmerRegStatus.Pending && x.Id == model.ProduceTypeId);

            if (produceType == null)
            {
                return new BaseResponse<ProduceTypeDto>
                {
                    Message = "Not Successful",
                    Status = false,
                };
            }

            produceType.Status = (Status)model.Status;
            _farmerProduceTypeRepository.Update(produceType);
            await _farmerProduceTypeRepository.SaveAsync();
            return new BaseResponse<ProduceTypeDto>
            {
                Message = "Successful",
                Status = true,
            };

        }


        public async Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetProduceTypesToBeApprovedAsync(Guid farmerId)
        {
            var produceTypes = await _farmerProduceTypeRepository.GetAllAsync(f => f.FarmerId == farmerId && f.IsDeleted == false);

            if (produceTypes == null)
            {
                return new BaseResponse<IEnumerable<ProduceTypeDto>>
                {
                    Message = "Not Found",
                    Status = false,
                };
            }

            return new BaseResponse<IEnumerable<ProduceTypeDto>>
            {
                Message = "Successful",
                Status = true,
                Data = produceTypes.Select(x => new ProduceTypeDto
                {
                    Id = x.ProduceType.Id,
                    TypeName = x.ProduceType.TypeName,
                    FarmerId = x.FarmerId
                    // FarmerIds = x.ProduceType.FarmerProduceTypes
                    //     .Select(farmerProduceType => farmerProduceType.FarmerId)
                    //     .ToList()
                })
            };
        }


        public async Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetAllApprovedProducetypesOfAFarmerAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            var farmerId = user.Farmer.Id;

            var approvedProduceTypes = await _farmerProduceTypeRepository.GetAllAsync(f => f.FarmerId == farmerId && !f.IsDeleted && f.Status == Status.Approved);

            if (!approvedProduceTypes.Any())
            {
                return new BaseResponse<IEnumerable<ProduceTypeDto>>
                {
                    Message = "Not Found",
                    Status = false,
                };
            }

            return new BaseResponse<IEnumerable<ProduceTypeDto>>
            {
                Message = "Successful",
                Status = true,
                Data = approvedProduceTypes.Select(x => new ProduceTypeDto
                {
                    Id = x.ProduceType.Id,
                    TypeName = x.ProduceType.TypeName,
                    FarmerId = x.FarmerId,
                    // ProduceName = x.ProduceType.Produce.ProduceName,
                    // NameOfCategory = x.ProduceType.Produce.Category.NameOfCategory,

                })
            };
        }
        public async Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetAllUnapprovedProducetypesOfAFarmerAsync(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            var farmerId = user.Farmer.Id;

            var allProduceTypes = await _produceTypeRepository.GetAllAsync();

            var approvedProduceTypes = await _farmerProduceTypeRepository.GetAllAsync(f => f.FarmerId == farmerId && !f.IsDeleted && f.Status == Status.Approved);
            var approvedProduceTypesIds = approvedProduceTypes.Select(p => p.ProduceTypeId).ToList();
            var unapprovedProduceTypes = allProduceTypes.Where(pt => !approvedProduceTypesIds.Contains(pt.Id)).ToList();

            if (!unapprovedProduceTypes.Any())
            {
                return new BaseResponse<IEnumerable<ProduceTypeDto>>
                {
                    Message = "Not Found",
                    Status = false,
                };
            }

            return new BaseResponse<IEnumerable<ProduceTypeDto>>
            {
                Message = "Successful",
                Status = true,
                Data = unapprovedProduceTypes.Select(x => new ProduceTypeDto
                {
                    Id = x.Id,
                    TypeName = x.TypeName,
                    // FarmerId = x.FarmerId,
                    // ProduceName = x.ProduceType.Produce.ProduceName,
                    // NameOfCategory = x.ProduceType.Produce.Category.NameOfCategory,

                })
            };
        }

       
        public async Task<BaseResponse<IEnumerable<ProduceTypeDto>>> GetApprovedProduceTypesForAFarmerByFarmerIdAsync(Guid farmerId)
        {
            var approvedProduceTypes = await _farmerProduceTypeRepository.GetAllApprovedProduceTypeOfAFarmer(farmerId);

            if (!approvedProduceTypes.Any())
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
                Data = approvedProduceTypes.Select(x => new ProduceTypeDto
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



    }
}