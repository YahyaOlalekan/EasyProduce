using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Abstractions.RepositoryInterfaces;
using Application.Dtos;
using Application.Dtos.PaymentGatewayDTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Persistence.Payments
{
    public class FlutterwaveService : IFlutterwaveService
    {
        private readonly string baseUrl = "https://api.flutterwave.com/v3";
        private readonly ITransactionRepository _transactionRepository;
        private readonly IConfiguration _config;

        public FlutterwaveService(
            ITransactionRepository transactionRepository,
            IConfiguration config
        )
        {
            _transactionRepository = transactionRepository;
            _config = config;
        }

        public async Task<PayoutResponse> InitiatePayoutAsync(Guid transactionId)
        {
            string secretKey = _config["Flutterwave:SecreteKey"];

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
                //account_bank = transaction.Farmer.BankCode,
                //account_number = transaction.Farmer.AccountNumber,
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

        public async Task<BaseResponse<GeneratedOtpResponseModel>> GenerateOTPAsync()
        {
            try
            {
                var url = "https://api.flutterwave.com/v3/otps";
                // var publicKey = "FLWPUBK_TEST-423f24968dece0d4bdefedc6c408094d-X";
                string secretKey = _config["Flutterwave:SecreteKey"];

                var data = new GenerateOTPRequestModel
                {
                    length = 7,
                    customer = new Customer
                    {
                        name = "EasyProduce",
                        email = "mybluvedcreator@gmail.com",
                        // phone = "2348000000000"
                        phone = "2348156151018"
                    },
                    sender = "Flutterwave Inc.",
                    send = true,
                    medium = new[] { "email", "whatsapp", "sms" },
                    expiry = 5
                };

                var client = new HttpClient();
                
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
                client
                    .DefaultRequestHeaders
                    .TryAddWithoutValidation("Content-Type", "application/json");

                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var generatedOtpResponse =
                        JsonConvert.DeserializeObject<GeneratedOtpResponseModel>(responseBody);

                    return new BaseResponse<GeneratedOtpResponseModel>
                    {
                        Message = "OTP generated successfully",
                        Status = true,
                        Data = generatedOtpResponse
                    };
                }
                else
                {
                    return new BaseResponse<GeneratedOtpResponseModel>
                    {
                        Message = $"Error: {responseBody}",
                        Status = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<GeneratedOtpResponseModel>
                {
                    Message = $"Exception: {ex.Message}",
                    Status = false
                };
            }
        }

        public async Task<BaseResponse<string>> ValidateOtpAsync(string otp)
        {
            try
            {
                var url = "https://api.flutterwave.com/v3/otps/verify";
                var publicKey = "FLWPUBK_TEST-423f24968dece0d4bdefedc6c408094d-X";
                string secretKey = _config["Flutterwave:SecreteKey"];
                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
                client
                    .DefaultRequestHeaders
                    .TryAddWithoutValidation("Content-Type", "application/json");

                var data = new { otp, public_key = publicKey };

                var jsonData = JsonConvert.SerializeObject(data);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new BaseResponse<string>
                    {
                        Message = "OTP verified successfully",
                        Status = true
                    };
                }
                else
                {
                    return new BaseResponse<string>
                    {
                        Message = $"Error: {responseBody}",
                        Status = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<string>
                {
                    Message = $"Exception: {ex.Message}",
                    Status = false
                };
            }
        }


        
    }
}
