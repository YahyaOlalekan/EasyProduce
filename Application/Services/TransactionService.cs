using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Application.Dtos.PaymentGatewayDTOs;
using Domain.Entity;

namespace Application.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IFarmerRepository _farmerRepository;
        private readonly IFarmerProduceTypeRepository _farmerProduceTypeRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IPayStackService _paystackService;
        private readonly IFlutterwaveService _flutterwaveService;

        public TransactionService(
            IFarmerRepository farmerRepository,
            IFarmerProduceTypeRepository farmerProduceTypeRepository,
            ITransactionRepository transactionRepository,
            IPayStackService paystackService,
            IFlutterwaveService flutterwaveService
        )
        {
            _farmerRepository = farmerRepository;
            _farmerProduceTypeRepository = farmerProduceTypeRepository;
            _transactionRepository = transactionRepository;
            _paystackService = paystackService;
            _flutterwaveService = flutterwaveService;
        }

        public async Task<BaseResponse<Transaction>> InitiateProducetypeSalesAsync(
            Guid farmerId,
            InitiateProducetypeSalesRequestModel model
        )
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

            var farmerProduceType = await _farmerProduceTypeRepository.GetAsync(
                x =>
                    x.FarmerId == farmer.Id
                    && x.ProduceTypeId == model.ProduceTypeId
                    && x.Status == Domain.Enum.Status.Approved
            );
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

            return new BaseResponse<Transaction> { Message = "Successful", Status = true };
        }

        public async Task<
            BaseResponse<IEnumerable<TransactionDto>>
        > GetAllInitiatedProducetypeSalesAsync()
        {
            var initiatedProducetypeSales =
                await _transactionRepository.GetAllInitiatedProducetypeSalesAsync();
            if (initiatedProducetypeSales.Any())
            {
                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = initiatedProducetypeSales.Select(
                        x =>
                            new TransactionDto
                            {
                                Id = x.Id,
                                ProduceTypeId = x.ProduceTypeId,
                                Price = x.Price,
                                Quantity = x.Quantity,
                                UnitOfMeasurement = x.UnitOfMeasurement,
                                TotalAmount = x.Price * (decimal)x.Quantity,
                                TransactionStatus = Domain.Enum.TransactionStatus.Initialized,
                                FarmerId = x.FarmerId,
                                RegistrationNumber = x.Farmer.RegistrationNumber,
                                TransactionNum = x.TransactionNum
                            }
                    )
                };
            }

            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Message = "No Initiated Producetypes Sales",
                Status = false,
                Data = null
            };
        }

        public async Task<
            BaseResponse<IEnumerable<TransactionDto>>
        > GetAllConfirmedProducetypeSalesAsync()
        {
            var confirmedProducetypeSales =
                await _transactionRepository.GetAllConfirmedProducetypeSalesAsync();
            if (confirmedProducetypeSales.Any())
            {
                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = confirmedProducetypeSales.Select(
                        x =>
                            new TransactionDto
                            {
                                Id = x.Id,
                                ProduceTypeId = x.ProduceTypeId,
                                Price = x.Price,
                                Quantity = x.Quantity,
                                UnitOfMeasurement = x.UnitOfMeasurement,
                                TotalAmount = x.Price * (decimal)x.Quantity,
                                TransactionStatus = Domain.Enum.TransactionStatus.Confirmed,
                                FarmerId = x.FarmerId,
                                RegistrationNumber = x.Farmer.RegistrationNumber,
                                TransactionNum = x.TransactionNum,
                                //  AccountName = x.Farmer.AccountName,
                                //  BankName = x.Farmer.BankName,
                                //  AccountNumber = x.Farmer.AccountNumber,
                                //  BankCode = x.Farmer.BankCode,
                            }
                    )
                };
            }

            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Message = "No Initiated Producetypes Sales",
                Status = false,
                Data = null
            };
        }

        // public async Task<BaseResponse<TransactionDto>> GenerateReceiptAsync(Guid transactionId)
        // {
        //     var transaction = await _transactionRepository.GetAsync(transactionId);
        //     if (transaction == null)
        //     {
        //         return new BaseResponse<TransactionDto>
        //         {
        //             Message = "Transaction is not found",
        //             Status = false,
        //             Data = null
        //         };
        //     }

        //     // if (transaction.TransactionStatus != Domain.Enum.TransactionStatus.Confirmed)
        //     // {
        //     //     return new BaseResponse<TransactionDto>
        //     //     {
        //     //         Message = "This transaction has not been confirmed",
        //     //         Status = false,
        //     //     };
        //     // }

        //     if (transaction.TransactionStatus != Domain.Enum.TransactionStatus.Paid)
        //     {
        //         return new BaseResponse<TransactionDto>
        //         {
        //             Message = "Seller has not been paid!",
        //             Status = false,
        //         };
        //     }

        //     return new BaseResponse<TransactionDto>
        //     {
        //         Message = "Successful",
        //         Status = true,
        //         Data = new TransactionDto
        //         {
        //             TransactionNum = transaction.TransactionNum,
        //             // TypeName = transaction.p
        //             UnitOfMeasurement = transaction.UnitOfMeasurement,
        //             Price = transaction.Price,
        //             Quantity = transaction.Quantity,
        //             TotalAmount = transaction.Price * (decimal)transaction.Quantity,
        //             TransactionStatus = Domain.Enum.TransactionStatus.Paid,
        //             RegistrationNumber = transaction.Farmer.RegistrationNumber,
        //             AccountName = transaction.Farmer.AccountName,
        //             BankName = transaction.Farmer.BankName,
        //             AccountNumber = transaction.Farmer.AccountNumber,
        //              DateCreated = DateTime.Now
        //             //  BankCode = transaction.Farmer.BankCode,
        //             // ProduceTypeId = transaction.ProduceTypeId,
        //             // FarmerId = transaction.FarmerId,
        //             // Id = transaction.Id,
        //         }
        //     };
        // }

        public async Task<
            BaseResponse<IEnumerable<TransactionDto>>
        > GetAllTransactionStatusForProducetypeSalesAsync()
        {
            var initiatedProducetypeSales = await _transactionRepository.GetAllAsync();
            if (initiatedProducetypeSales.Any())
            {
                return new BaseResponse<IEnumerable<TransactionDto>>
                {
                    Message = "Successful",
                    Status = true,
                    Data = initiatedProducetypeSales.Select(
                        x =>
                            new TransactionDto
                            {
                                Id = x.Id,
                                ProduceTypeId = x.ProduceTypeId,
                                Price = x.Price,
                                Quantity = x.Quantity,
                                UnitOfMeasurement = x.UnitOfMeasurement,
                                TotalAmount = x.Price * (decimal)x.Quantity,
                                TransactionStatus = Domain.Enum.TransactionStatus.Initialized,
                                FarmerId = x.FarmerId,
                                RegistrationNumber = x.Farmer.RegistrationNumber,
                                TransactionNum = x.TransactionNum
                            }
                    )
                };
            }

            return new BaseResponse<IEnumerable<TransactionDto>>
            {
                Message = "No Initiated Producetypes Sales",
                Status = false,
                Data = null
            };
        }

        public async Task<BaseResponse<string>> VerifyInitiatedProducetypeSalesAsync(
            InitiatedProducetypeSalesRequestModel model
        )
        {
            var initiatedProducetypeSale = await _transactionRepository.GetAsync(
                t =>
                    t.Id == model.Id
                    && t.TransactionStatus == Domain.Enum.TransactionStatus.Initialized
            );

            if (initiatedProducetypeSale == null)
            {
                return new BaseResponse<string>
                {
                    Message = "Initiated Producetype Sales not found",
                    Status = false,
                };
            }

            initiatedProducetypeSale.TransactionStatus = model.TransactionStatus;

            if (model.TransactionStatus == Domain.Enum.TransactionStatus.Confirmed)
            {
                var publicKey = "FLWPUBK_TEST-423f24968dece0d4bdefedc6c408094d-X";
                var secretKey = "FLWSECK_TEST-c789b9f2217485eb647843281a337bce-X";
                var payoutResponse = await _flutterwaveService.InitiatePayoutAsync(
                    publicKey,
                    secretKey,
                    model.Id
                );

                if (!payoutResponse.IsSuccessful)
                {
                    return new BaseResponse<string> { Message = "Payout failed", Status = false, };
                }
            }
            // initiatedProducetypeSale.TransactionStatus = model.TransactionStatus;
            _transactionRepository.Update(initiatedProducetypeSale);
            await _transactionRepository.SaveAsync();

            return new BaseResponse<string> { Message = "Successful", Status = true };
        }

        // public async Task<BaseResponse<string>> VerifyInitiatedProducetypeSalesAsync(InitiatedProducetypeSalesRequestModel model )
        // {
        //     var initiatedProducetypeSale = await _transactionRepository.GetAsync( t =>
        //             t.Id == model.Id
        //             && t.TransactionStatus == Domain.Enum.TransactionStatus.Initialized
        //     );

        //     if (initiatedProducetypeSale == null)
        //     {
        //         return new BaseResponse<string>
        //         {
        //             Message = "Initiated Producetype Sales not found",
        //             Status = false,
        //         };
        //     }

        //     initiatedProducetypeSale.TransactionStatus = model.TransactionStatus;
        //     _transactionRepository.Update(initiatedProducetypeSale);
        //     await _transactionRepository.SaveAsync();

        //     return new BaseResponse<string> { Message = "Successful", Status = true };
        // }





        public async Task<BaseResponse<string>> ProcessPaymentAsync(Guid transactionId)
        {
            var transaction = await _transactionRepository.GetAsync(transactionId);
            if (transaction == null)
            {
                return new BaseResponse<string>
                {
                    Message = "Transaction is not found",
                    Status = false,
                };
            }

            if (transaction.TransactionStatus != Domain.Enum.TransactionStatus.Confirmed)
            {
                return new BaseResponse<string>
                {
                    Message = "This transaction has not been confirmed",
                    Status = false,
                };
            }

            var transferRecipientModel = new CreateTransferRecipientRequestModel
            {
                Name = transaction.Farmer.AccountName,
                AccountNumber = transaction.Farmer.AccountNumber,
                BankCode = transaction.Farmer.BankCode,
            };
            var transferRecipient = await _paystackService.CreateTransferRecipient(
                transferRecipientModel
            );

            var transferRecipientResponseModel = new InitiateTransferRequesteModel
            {
                Amount = transaction.TotalAmount,
                RecipientCode = transferRecipient.data.recipient_code
            };
            var initiateTransfer = await _paystackService.InitiateTransfer(
                transferRecipientResponseModel
            );

            // var finalizeTransfer = await _paystackService.FinalizeTransfer(initiateTransfer.data.transfer_code, initiateTransfer);

            return new BaseResponse<string>
            {
                Message = " ",
                Status = true,
                Data = initiateTransfer.data.transfer_code
            };
        }

        // public async Task<BaseResponse<string>> ReceiveAnOtpAsync(string transferCode, string otp)
        public async Task<BaseResponse<string>> MakePaymentAsync(string transferCode, string otp)
        {
            var finalizeTransfer = await _paystackService.FinalizeTransfer(transferCode, otp);

            if (finalizeTransfer.data.status.Equals("success"))
            {
                //change transaction status to paid

                return new BaseResponse<string>
                {
                    Message = "Money sent successfuly",
                    Status = true,
                };
            }

            return new BaseResponse<string> { Message = "Transfer failed ", Status = true, };
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


        // public async Task<BaseResponse<Transaction>> ConfirmPaymentAsync(string otp,)
        // {
        //     var transaction = await _transactionRepository.GetAsync(f => f.Id == transactionId);
        //     if (transaction == null)
        //     {
        //         return new BaseResponse<Transaction>
        //         {
        //             Message = "Farmer Not Found!",
        //             Status = false
        //         };
        //     }
        //     var initRequest = new InitiateTransferRequesteModel
        //     {
        //         Amount = transaction.TotalAmount,
        //         RecipientCode

        //     };
        //     var init = await _paystackService.InitiateTransfer();
        // }
    }
}
