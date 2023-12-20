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
        Task<BaseResponse<Transaction>> InitiateProducetypeSalesAsync(Guid farmerId, InitiateProducetypeSalesRequestModel model);
        Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllInitiatedProducetypeSalesAsync();
        Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllConfirmedProducetypeSalesAsync();
        // Task<BaseResponse<Transaction>> ConfirmPaymentAsync(Guid transactionId);
        Task<BaseResponse<string>> VerifyInitiatedProducetypeSalesAsync(InitiatedProducetypeSalesRequestModel model);
        Task<BaseResponse<string>> ProcessPaymentAsync(Guid transactionId);
        // Task<BaseResponse<string>> ReceiveAnOtpAsync(string transferCode, string otp);
        Task<BaseResponse<string>> MakePaymentAsync(string transferCode, string otp);
    //    Task<BaseResponse<TransactionDto>> GenerateReceiptAsync(Guid transactionId);
       

        // Task<string> SellProduceType(Guid farmerId, SellProduceTypeRequestModel model);
        // Task<BaseResponse<TransactionDto>> ApprovedAsync(Guid userId, Guid id);
        // Task<BaseResponse<TransactionDto>> DeclinedAsync(Guid userId, Guid id);
        // Task<BaseResponse<TransactionDto>> CreateAsync(CreateTransactionRequestModel model);
        // Task<BaseResponse<TransactionDto>> DeliveredAsync(Guid id);
        // Task<BaseResponse<TransactionDto>> NotDeliveredAsync(Guid id);
        // Task<BaseResponse<TransactionDto>> UpdateAsync(Guid id, UpdateTransactionRequestModel model);
        // Task<BaseResponse<TransactionDto>> GetAsync(Guid id);
        // Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllAsync();
        // Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllByUserIdAsync(Guid userId);
        // Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllByStatusAsync(TransactionStatus status);
        // Task<BaseResponse<TransactionDto>> DeleteAsync(Guid id);
        // Task<BaseResponse<TransactionDto>> DetailsAsync(Guid transactionNum);
    }
}