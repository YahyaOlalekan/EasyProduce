using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Abstractions.RepositoryInterfaces;
using Application.Dtos;
using Application.Dtos.PaymentGatewayDTOs;
using RestSharp;

namespace Persistence.Payments
{
    public class FlutterwaveService : IFlutterwaveService
    {
        private readonly string baseUrl = "https://api.flutterwave.com/v3";
        private readonly ITransactionRepository _transactionRepository;

        public FlutterwaveService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<PayoutResponse> InitiatePayoutAsync(
            string publicKey,
            string secretKey,
            Guid transactionId
        )
        {
            var client = new RestClient($"{baseUrl}/transfers");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", $"Bearer {secretKey}");
            request.AddHeader("Content-Type", "application/json");

            var transaction = await _transactionRepository.GetAsync(transactionId);
            if (transaction == null)
            {
                return new PayoutResponse
                {
                    IsSuccessful = false,
                    ErrorMessage = "Transaction is not found",
                    OriginalResponse = null
                };
            }

           
            var model = new PayoutModel
            {
                account_bank = "044",
                account_number = "0690000040",
                amount = transaction.TotalAmount,
                narration = "Payment for producetype purchased",
                currency = "NGN",
                reference = Guid.NewGuid().ToString() + "_PMCKDU_1",
                callback_url = "https://www.flutterwave.com/ng/",
                debit_currency = "NGN"
            };

            request.AddJsonBody(model);

            var originalResponse = client.Execute(request);

            if (originalResponse.IsSuccessful)
            {
                 transaction.TransactionStatus = Domain.Enum.TransactionStatus.Confirmed;
                await _transactionRepository.SaveAsync();
            }

            return new PayoutResponse
            {
                IsSuccessful = originalResponse.IsSuccessful,
                ErrorMessage = originalResponse.ErrorMessage,
                OriginalResponse = originalResponse
            };
        }


/////////////////////////////////////

        // public async Task<PayoutResponse> InitiatePayoutAsync(
        //     string publicKey,
        //     string secretKey,
        //     Guid transactionId
        // )
        // {
        //     var client = new RestClient($"{baseUrl}/transfers");
        //     var request = new RestRequest(Method.POST);
        //     request.AddHeader("Authorization", $"Bearer {secretKey}");
        //     request.AddHeader("Content-Type", "application/json");

        //     var transaction = await _transactionRepository.GetAsync(transactionId);
        //     if (transaction == null)
        //     {
        //         return new PayoutResponse
        //         {
        //             IsSuccessful = false,
        //             ErrorMessage = "Transaction is not found",
        //             OriginalResponse = null
        //         };
        //     }

        //     if (transaction.TransactionStatus != Domain.Enum.TransactionStatus.Confirmed)
        //     {
        //         return new PayoutResponse
        //         {
        //             IsSuccessful = false,
        //             ErrorMessage = "This transaction has not been confirmed",
        //             OriginalResponse = null
        //         };
        //     }

        //     var model = new PayoutModel
        //     {
        //         account_bank = "044",
        //         account_number = "0690000040",
        //         amount = transaction.TotalAmount,
        //         narration = "Payment for producetype purchased",
        //         currency = "NGN",
        //         reference = Guid.NewGuid().ToString() + "_PMCKDU_1",
        //         callback_url = "https://www.flutterwave.com/ng/",
        //         debit_currency = "NGN"
        //     };

        //     request.AddJsonBody(model);

        //     var originalResponse = client.Execute(request);

        //     if (originalResponse.IsSuccessful)
        //     {
        //         transaction.TransactionStatus = Domain.Enum.TransactionStatus.Paid;
        //         await _transactionRepository.SaveAsync();
        //     }

        //     return new PayoutResponse
        //     {
        //         IsSuccessful = originalResponse.IsSuccessful,
        //         ErrorMessage = originalResponse.ErrorMessage,
        //         OriginalResponse = originalResponse
        //     };
        // }


        ///////////////////////////////

        // public async Task<IRestResponse> InitiatePayout(
        //     string publicKey,
        //     string secretKey,
        //     Guid transactionId
        // )
        // {
        //     var client = new RestClient($"{baseUrl}/transfers");
        //     var request = new RestRequest(Method.POST);
        //     request.AddHeader("Authorization", $"Bearer {secretKey}");
        //     request.AddHeader("Content-Type", "application/json");

        //     var transaction = await _transactionRepository.GetAsync(transactionId);
        //     // if (transaction == null)
        //     // {
        //     //     return new BaseResponse<string>
        //     //     {
        //     //         Message = "Transaction is not found",
        //     //         Status = false,
        //     //     };
        //     // }

        //     // if (transaction.TransactionStatus != Domain.Enum.TransactionStatus.Confirmed)
        //     // {
        //     //     return new BaseResponse<string>
        //     //     {
        //     //         Message = "This transaction has not been confirmed",
        //     //         Status = false,
        //     //     };
        //     // }

        //     var model = new PayoutModel
        //     {
        //         // account_bank = transaction.Farmer.BankCode,
        //         account_bank = "044",
        //         // account_number = transaction.Farmer.AccountNumber,
        //         account_number = "0690000040",
        //         amount = transaction.TotalAmount,
        //         narration = "Payment for producetype purchased",
        //         currency = "NGN",
        //         reference = Guid.NewGuid().ToString() + "_PMCKDU_1",
        //         callback_url = "https://www.flutterwave.com/ng/",
        //         debit_currency = "NGN"
        //     };

        //     request.AddJsonBody(model);

        //     return client.Execute(request);
        // }

        // public async Task<PayoutInitiationResponse> InitiatePayoutForFarmer(
        //     string publicKey,
        //     string secretKey,
        //     Guid transactionId
        // )
        // {
        //     var client = new RestClient($"{baseUrl}/transfers");
        //     var request = new RestRequest(Method.POST);
        //     request.AddHeader("Authorization", $"Bearer {secretKey}");
        //     request.AddHeader("Content-Type", "application/json");

        //     var transaction = await _transactionRepository.GetAsync(transactionId);

        //     var model = new PayoutModel
        //     {
        //         account_bank = "044",
        //         account_number = "0690000040",
        //         amount = transaction.TotalAmount,
        //         narration = "Payment for producetype purchased",
        //         currency = "NGN",
        //         reference = Guid.NewGuid().ToString(),
        //         callback_url = "https://www.flutterwave.com/ng/",
        //         debit_currency = "NGN"
        //     };

        //     request.AddJsonBody(model);

        //     // Check if OTP is required (you might have some conditions for this)
        //     // bool otpRequired = SomeConditionToDetermineOtpRequirement();

        //     var response = new PayoutInitiationResponse
        //     {
        //         OtpRequired = transaction != null,
        //         FlutterwaveResponse = client.Execute(request)
        //     };

        //     return response;
        // }



    }
}
