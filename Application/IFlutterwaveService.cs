using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using Application.Dtos.PaymentGatewayDTOs;

namespace Application
{
    public interface IFlutterwaveService
    {
        // Task<PayoutResponse> InitiatePayoutAsync(string publicKey, string secretKey, Guid transactionId );
        Task<PayoutResponse> InitiatePayoutAsync(Guid transactionId );
        // Task<BaseResponse<string>> GenerateOTPAsync();
        //  Task<BaseResponse<GeneratedOtpResponseModel>> GenerateOTPAsync( string publicKey, string secretKey, Customer customer );
         Task<BaseResponse<GeneratedOtpResponseModel>> GenerateOTPAsync();
        Task<BaseResponse<string>> ValidateOtpAsync(string otp );

    }
}