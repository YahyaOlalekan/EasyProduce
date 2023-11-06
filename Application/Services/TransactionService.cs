using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IFarmerProduceTypeRepository _farmerProduceTypeRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IFarmerRepository farmerRepository, IFarmerProduceTypeRepository farmerProduceTypeRepository, ITransactionRepository transactionRepository)
        {
            _farmerRepository = farmerRepository;
            _farmerProduceTypeRepository = farmerProduceTypeRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<BaseResponse<Transaction>> InitiateProducetypeSales(Guid farmerId, InitiateProducetypeSalesRequestModel model)
        {
            var farmer = await _farmerRepository.GetAsync(f => f.UserId == farmerId);
            if (farmer == null)
            {
                return new BaseResponse<Transaction>
                {
                    Message = "Farmer Not Found!",
                    Status = false
                };

            }

            if (farmer.FarmerRegStatus != Domain.Enum.FarmerRegStatus.Approved)
            {
                return new BaseResponse<Transaction>
                {
                    Message = "Sorry, You are yet to be approved as a produce type Seller",
                    Status = false
                };
            }

            var farmerProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmer.Id && x.ProduceTypeId == model.ProduceTypeId && x.Status == Domain.Enum.Status.Approved);
            if (farmerProduceType == null)
            {
                return new BaseResponse<Transaction>
                {
                    Message = "Sorry, this produce Type is not approved for you",
                    Status = false
                };
            }

            var transaction = new Transaction
            {
                ProduceTypeId = model.ProduceTypeId,
                Price = model.Price,
                Quantity = model.Quantity,
                UnitOfMeasurement = model.UnitOfMeasurement,
                TotalAmount = model.Price * (decimal)model.Quantity,
                TransactionStatus = Domain.Enum.TransactionStatus.Initialized,
                FarmerId = farmer.Id,
                TransactionNum = await _transactionRepository.GenerateTransactionRegNumAsync()
            };

            await _transactionRepository.CreateTransactionAsync(transaction);
            await _transactionRepository.SaveAsync();

            return new BaseResponse<Transaction>
            {
                Message = "Successful",
                Status = true
            };
        }



        // public async Task<string> InitiateProducetypeSales(Guid farmerId, InitiateProducetypeSalesRequestModel model)
        // {
        //     var farmer = await _farmerRepository.GetAsync(f => f.Id == farmerId);
        //     if (farmer == null)
        //     {
        //         return "Farmer Not Found!";
        //     }

        //     if (farmer.FarmerRegStatus != Domain.Enum.FarmerRegStatus.Approved)
        //     {
        //         return "Sorry, You are yet to be approved as a produce type Seller";
        //     }

        //     var farmerProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmerId && x.ProduceTypeId == model.ProduceTypeId && x.Status == Domain.Enum.Status.Approved);
        //     if (farmerProduceType == null)
        //     {
        //         return "Sorry, this produce Type is not approved for you";
        //     }

        //     var transaction = new Transaction
        //     {
        //         ProduceTypeId = model.ProduceTypeId,
        //         Price = model.Price,
        //         Quantity = model.Quantity,
        //         UnitOfMeasurement = model.UnitOfMeasurement,
        //         TotalAmount = model.Price * (decimal)model.Quantity,
        //         TransactionStatus = Domain.Enum.TransactionStatus.Initialized,
        //         FarmerId = farmerId,
        //         TransactionNum = await _transactionRepository.GenerateTransactionRegNumAsync()
        //     };

        //     await _transactionRepository.CreateTransactionAsync(transaction);
        //     await _transactionRepository.SaveAsync();

        //     return "Successful";
        // }


        // public async Task<string> PriceConfirmAsync(Guid farmerId, PriceConfirmRequestModel model)
        // {
        //     var farmer = await _farmerRepository.GetAsync(f => f.Id == farmerId);
        //     if (farmer == null)
        //     {
        //         return "Farmer Not Found!";
        //     }

        //     if (farmer.FarmerRegStatus != Domain.Enum.FarmerRegStatus.Approved)
        //     {
        //         return "Sorry, You are yet to be approved as a produce type Seller";
        //     }

        //     var farmerProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmerId && x.ProduceTypeId == model.ProduceTypeId && x.Status == Domain.Enum.Status.Approved);
        //     if (farmerProduceType == null)
        //     {
        //         return "Sorry, this produce Type is not approved for you";
        //     }



        // }




        //  public async Task<BaseResponse<string>> VerifySellingOfProduceAsync(ApproveFarmerDto model)
        // {
        //     var farmer = await _farmerRepository.GetAsync(model.Id);

        //     if (farmer == null)
        //     {
        //         return new BaseResponse<string>
        //         {
        //             Message = "Farmer not found",
        //             Status = false,
        //         };
        //     }

        //     farmer.FarmerRegStatus = model.Status;
        //     _farmerRepository.Update(farmer);
        //     await _farmerRepository.SaveAsync();

        //     return new BaseResponse<string>
        //     {
        //         Message = "Successful",
        //         Status = true
        //     };
        // }


        // public async Task<BaseResponse<IEnumerable<FarmerDto>>> GetFarmersByStatusAsync(FarmerStatusRequestModel model)
        // {
        //     var farmers = await _farmerRepository.GetSelectedAsync(f => f.FarmerRegStatus == model.Status && !f.IsDeleted);

        //     if (!farmers.Any())
        //     {
        //         return new BaseResponse<IEnumerable<FarmerDto>>
        //         {
        //             Message = "Farmer not found",
        //             Status = false,
        //         };
        //     }

        //     return new BaseResponse<IEnumerable<FarmerDto>>
        //     {
        //         Message = "Successful",
        //         Status = true,
        //         Data = farmers.Select(f => new FarmerDto
        //         {
        //             Id = f.Id,
        //             RegistrationNumber = f.RegistrationNumber,
        //             FirstName = f.User.FirstName,
        //             LastName = f.User.LastName,
        //             Email = f.User.Email,
        //             PhoneNumber = f.User.PhoneNumber,
        //             Address = f.User.Address,
        //             ProfilePicture = f.User.ProfilePicture,
        //         })
        //     };
        // }



        //  public async Task<BaseResponse<IEnumerable<FarmerDto>>> GetAllFarmersAsync()
        // {
        //     var farmers = await _farmerRepository.GetAllAsync();
        //     if (!farmers.Any())
        //     {
        //         return new BaseResponse<IEnumerable<FarmerDto>>
        //         {
        //             Message = "No farmer found",
        //             Status = false
        //         };
        //     }

        //     return new BaseResponse<IEnumerable<FarmerDto>>
        //     {
        //         Message = "Successful",
        //         Status = true,
        //         Data = farmers.Select(m => new FarmerDto
        //         {
        //             // Id = m.Id,
        //             RegistrationNumber = m.RegistrationNumber,
        //             FirstName = m.User.FirstName,
        //             LastName = m.User.LastName,
        //             Email = m.User.Email,
        //             PhoneNumber = m.User.PhoneNumber,
        //             Address = m.User.Address,
        //             FarmName = m.FarmName,

        //             // ProfilePicture = m.User.ProfilePicture,
        //         })
        //     };
        // }


        //  public async Task<BaseResponse<FarmerProduceTypeDto>> ProduceTypeDetailsToBeSoldByTheFarmer(Guid farmerId, ProduceTypeDetailsToBeSoldByTheFarmerRequestModel model)
        //         {
        //             // var farmer = await _farmerRepository.GetAsync(f => f.Id == farmerId && f.FarmerRegStatus == Domain.Enum.FarmerRegStatus.Approved);
        //             // var produceType = await _produceTypeRepository.GetAsync(pT => pT.Id == model.Id && pT.Status == Domain.Enum.Status.Approved);
        //              var farmerProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmerId && x.Farmer.FarmerRegStatus == Domain.Enum.FarmerRegStatus.Approved && x.ProduceTypeId == model.Id && x.Status == Domain.Enum.Status.Approved);

        //             if (farmerProduceType == null)
        //             {
        //                 return new BaseResponse<FarmerProduceTypeDto>
        //                 {
        //                     Message = "Not successful",
        //                     Status = false
        //                 };
        //             }

        //         var produceType = new ProduceType();
        //            produceType.Id = model.Id;
        //            produceType.SellingPrice = model.Price;
        //            produceType.QuantityToBuy = model.Quantity;
        //            produceType.UnitOfMeasurement = model.UnitOfMeasurement;

        //         //    await _farmerfarmerProduceTypeRepository.Update(produceType);
        //             _produceTypeRepository.Update(produceType);
        //            await _produceTypeRepository.SaveAsync();

        //         }




        // var approvedProduceTypes = await _farmerProduceTypeRepository.GetAllApprovedProduceTypeOfAFarmer(farmerId);
        // if (!approvedProduceTypes.Any())
        // {
        //     return $"{farmer.User.FirstName}, you don't have any approved produce type for selling!";
        // }

        // foreach (var produceType in approvedProduceTypes)
        // {
        //     var transaction = await _transactionRepository.GetAsync(a => a.TransactionNum == model.TransactionNum);
        //     if (transaction == null)
        //     {
        //         produceType.Id = model.ProduceTypeId;
        //         produceType.SellingPrice = model.Price;
        //         produceType.Quantity = model.Quantity;
        //         produceType.UnitOfMeasurement = model.UnitOfMeasurement;

        //  }
        // }



        // var farmerProduceType = await _farmerProduceTypeRepository.GetAsync(x => x.FarmerId == farmerId && x.ProduceTypeId == model.ProduceTypeId && x.Status == Domain.Enum.Status.Approved);

        // if (farmerProduceType == null)
        // {
        //     return "Sorry, the produce type is yet to be approved";
        // }

        // farmerProduceType.ProduceType.ProduceId = model.ProduceTypeId;
        // farmerProduceType.ProduceType.CostPrice = model.Price;
        // farmerProduceType.ProduceType.Quantity = model.Quantity;
        // farmerProduceType.ProduceType.UnitOfMeasurement = model.UnitOfMeasurement;
        // // farmerProduceType.ProduceType.TransactionStatus = model.TransactionStatus.;
        // // farmerProduceType.ProduceType.CreatedBy = farmer.User.FirstName;
        // // farmerProduceType.ProduceType.DateCreated = DateTime.Now;

        // _farmerProduceTypeRepository.Update(farmerProduceType);
        // await _farmerProduceTypeRepository.SaveAsync();


    }
}