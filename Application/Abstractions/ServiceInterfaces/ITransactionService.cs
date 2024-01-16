using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Entity;
using Domain.Enum;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface ITransactionService
    {
        Task<BaseResponse<Transaction>> InitiateProducetypeSalesAsync(
            Guid farmerId,
            InitiateProducetypeSalesRequestModel model
        );
        Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllInitiatedProducetypeSalesAsync();
        Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllConfirmedProducetypeSalesAsync();
        Task<BaseResponse<TransactionDto>> GenerateReceiptAsync(Guid transactionId);
        Task<BaseResponse<string>> VerifyInitiatedProducetypeSalesAsync(
            InitiatedProducetypeSalesRequestModel model
        );
        Task<BaseResponse<string>> ProcessPaymentAsync(Guid transactionId);
        Task<BaseResponse<string>> MakePaymentAsync(string transferCode, string otp);


       
    }
}
