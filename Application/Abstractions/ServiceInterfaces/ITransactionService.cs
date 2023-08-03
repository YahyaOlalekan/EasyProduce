using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Enum;

namespace Application.Abstractions.ServiceInterfaces
{
    public interface ITransactionService
    {
        Task<BaseResponse<TransactionDto>> ApprovedAsync(Guid userId, Guid id);
        Task<BaseResponse<TransactionDto>> DeclinedAsync(Guid userId, Guid id);
        Task<BaseResponse<TransactionDto>> CreateAsync(CreateTransactionRequestModel model);
        Task<BaseResponse<TransactionDto>> DeliveredAsync(Guid id);
        Task<BaseResponse<TransactionDto>> NotDeliveredAsync(Guid id);
        Task<BaseResponse<TransactionDto>> UpdateAsync(Guid id, UpdateTransactionRequestModel model);
        Task<BaseResponse<TransactionDto>> GetAsync(Guid id);
        Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllAsync();
        Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllByUserIdAsync(Guid userId);
        Task<BaseResponse<IEnumerable<TransactionDto>>> GetAllByStatusAsync(TransactionStatus status);
        Task<BaseResponse<TransactionDto>> DeleteAsync(Guid id);
        Task<BaseResponse<TransactionDto>> DetailsAsync(Guid transactionNum);
    }
}