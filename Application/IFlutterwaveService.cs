using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.PaymentGatewayDTOs;

namespace Application
{
    public interface IFlutterwaveService
    {
        Task<PayoutResponse> InitiatePayoutAsync(string publicKey, string secretKey, Guid transactionId );
    }
}