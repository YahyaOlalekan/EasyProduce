using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Transactions;
using Application.Abstractions;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Application.Dtos.PaymentGatewayDTOs;
using Flutterwave.Ravepay.Net;
using Flutterwave.Ravepay.Net.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persistence.Payments;


namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IPayStackService _payStackService;
        private readonly ITransactionRepository _transactionRepo;
        // private readonly IHttpClientFactory _httpClientFactory;
        private readonly FlutterwaveService _flutterwaveService;
        public TransactionController(ITransactionService transactionService, IPayStackService payStackService, ITransactionRepository transactionRepo, FlutterwaveService flutterwaveService)
        {
            _transactionService = transactionService;
            _payStackService = payStackService;
            _transactionRepo = transactionRepo;
            _flutterwaveService = flutterwaveService;
        }



        [HttpPost("InitiatePayoutAsync/{transactionId}")]
        public async Task<IActionResult> InitiatePayoutAsync([FromRoute] Guid transactionId)
        {

            var publicKey = "FLWPUBK_TEST-423f24968dece0d4bdefedc6c408094d-X";
            var secretKey = "FLWSECK_TEST-c789b9f2217485eb647843281a337bce-X";

            var response = await _flutterwaveService.InitiatePayoutAsync(publicKey, secretKey, transactionId);

            if (response.IsSuccessful)
            {
                return Ok(response.OriginalResponse.Content);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
                // return BadRequest(response.Content);
            }
        }

        [HttpPost("initiate/{transactionId}")]
        public async Task<IActionResult> InitiatePayout([FromRoute] Guid transactionId)
        {

            var publicKey = "FLWPUBK_TEST-423f24968dece0d4bdefedc6c408094d-X";
            var secretKey = "FLWSECK_TEST-c789b9f2217485eb647843281a337bce-X";

            var response = await _flutterwaveService.InitiatePayout(publicKey, secretKey, transactionId);

            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
                // return BadRequest(response.Content);
            }
        }


        // [HttpPost("InitiatePayoutForFarmer/{transactionId}")]
        // public async Task<IActionResult> InitiatePayoutForFarmer([FromRoute] Guid transactionId,[FromBody] OtpInputModel otpInput)
        // {
        //     var publicKey = "FLWPUBK_TEST-423f24968dece0d4bdefedc6c408094d-X";
        //     var secretKey = "FLWSECK_TEST-c789b9f2217485eb647843281a337bce-X";

        //     var response = await _flutterwaveService.InitiatePayoutForFarmer(publicKey, secretKey, transactionId);


        //     // if (response.OtpRequired)
        //     // {
        //     //     return StatusCode(428, "OTP is required for payout initiation.");
        //     // }

        //     if (response.OtpRequired)
        //     {
        //         if (string.IsNullOrWhiteSpace(otpInput?.Otp))
        //         {
        //             // If OTP is required but not provided, return a specific status code
        //             return StatusCode(428, "OTP is required for payout initiation.");
        //         }

        //         // Include the OTP in the Flutterwave service call
        //         response = await _flutterwaveService.CompletePayoutWithOtp(response, otpInput.Otp);
        //     }


        //     if (response.FlutterwaveResponse.IsSuccessful)
        //     {
        //         return Ok(response.FlutterwaveResponse.Content);
        //     }
        //     else
        //     {
        //         return BadRequest(response.FlutterwaveResponse.ErrorMessage);
        //     }


        // }








        [Authorize(Roles = "farmer")]
        [HttpPut("InitiateProducetypeSales/{farmerId}")]
        public async Task<IActionResult> InitiateProducetypeSalesAsync([FromRoute] Guid farmerId, [FromBody] InitiateProducetypeSalesRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _transactionService.InitiateProducetypeSalesAsync(farmerId, model);
                // TempData["message"] = result.Message;
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }

        [Authorize(Roles = "manager")]
        [HttpGet("GetAllInitiatedProducetypeSales")]
        public async Task<IActionResult> GetAllInitiatedProducetypeSalesAsync()
        {
            var initiatedProducetypeSales = await _transactionService.GetAllInitiatedProducetypeSalesAsync();
            if (initiatedProducetypeSales == null)
            {
                return NotFound(initiatedProducetypeSales);
            }
            return Ok(initiatedProducetypeSales);
        }

        [Authorize(Roles = "manager")]
        [HttpGet("GetAllConfirmedProducetypeSales")]
        public async Task<IActionResult> GetAllConfirmedProducetypeSalesAsync()
        {
            var confirmedProducetypeSales = await _transactionService.GetAllConfirmedProducetypeSalesAsync();
            if (confirmedProducetypeSales == null)
            {
                return NotFound(confirmedProducetypeSales);
            }
            return Ok(confirmedProducetypeSales);
        }

        [Authorize(Roles = "manager")]
        [HttpPost("VerifyInitiatedProducetypeSales")]
        public async Task<IActionResult> VerifyInitiatedProducetypeSalesAsync(InitiatedProducetypeSalesRequestModel model)
        {
            var result = await _transactionService.VerifyInitiatedProducetypeSalesAsync(model);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpGet("InitiatePayment/{transactionId}")]
        public async Task<IActionResult> InitiatePayment([FromRoute] Guid transactionId)
        {
            var transferCode = await _transactionService.ProcessPaymentAsync(transactionId);

            if (transferCode is not null)
            {
                return Ok(transferCode);
            }
            return NotFound(transferCode);
        }

        [HttpGet("FinalizePayment/{transactionId}")]
        public async Task<IActionResult> FinalizePayment([FromRoute] string transferCode, string otp)
        {
            var result = await _transactionService.MakePaymentAsync(transferCode, otp);

            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }



        // [HttpPost("initiate-transfer")]
        // public async Task<IActionResult> InitiateTransfer([FromBody] TransferRequest transferData)
        // {
        //     try
        //     {
        //         // TransferRequest transferData = new TransferRequest
        //         // {
        //         //     account_bank = "044",
        //         //     account_number = "0690000040",
        //         //     amount = 5500,
        //         //     narration = "Akhlm Pstmn Trnsfr xx007",
        //         //     currency = "NGN",
        //         //     reference = "akhlm-pstmnpyt-rfxx007_PMCKDU_1",
        //         //     callback_url = "https://www.flutterwave.com/ng/",
        //         //     debit_currency = "NGN"
        //         // };

        //         var client = _httpClientFactory.CreateClient();
        //         var baseUrl = "https://api.flutterwave.com"; // Update with the correct base URL

        //         var flwSecretKey = "FLWSECK_TEST-c789b9f2217485eb647843281a337bce-X"; // Replace with your actual Flutterwave secret key

        //         client.DefaultRequestHeaders.Add("Authorization", $"Bearer {flwSecretKey}");

        //         // var response = await client.PostAsJsonAsync($"{baseUrl}/v3/transfers", request);
        //          var response = await client.PostAsJsonAsync($"{baseUrl}/v3/transfers", transferData);

        //         response.EnsureSuccessStatusCode();

        //         // var result = await response.Content.ReadAsAsync<object>(); // Change object to the expected response type
        //         var result = await response.Content.ReadAsAsync<TransferResponse>();


        //         return Ok(result);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Internal server error: {ex.Message}");
        //     }
        // }


        // [HttpPost("FlutterwavePaymentTesting")]
        // public async Task<IActionResult> PaymentTesting()
        // {
        //     //    var encryptionKey = "FLWSECK_TEST7f77751a195f";
        //     // var secreteKey = "FLWSECK_TEST-c789b9f2217485eb647843281a337bce-X";
        //     var publicKey = "FLWPUBK_TEST-423f24968dece0d4bdefedc6c408094d-X";
        //     var raveConfig = new RavePayConfig(publicKey, false);
        //     var accountCharge = new RaveAccountCharge(raveConfig);
        //     // var accountParams = new AccountChargeParams(publicKey, "Anonymous", "customer", "user@example.com", "0690000031", 509,
        //     //  "acessBank.BankCode",  "txRef");

        //     var accountParams = new AccountChargeParams(publicKey, "Anonymous", "customer", "user@example.com", "0690000031", 200,
        //         "044", "dfs23fhr7ntg0293039_PMCKDU_1");

        //     var chargeResponse = await accountCharge.Charge(accountParams);

        //     // Now check the response received from the API, especially the validation status
        //     if (chargeResponse.Data.Status == "success-pending-validation")
        //     {
        //         // This usually means the user needs to validate the transaction with an OTP

        //         // Retrieve the OTP validation instructions
        //         var validateInstructions = chargeResponse.Data.ValidateInstructions;

        //         // Display the instructions to the user (you can customize this based on your application's UI)
        //         Console.WriteLine(validateInstructions.Instruction);

        //         // Display the flash message
        //         Console.WriteLine(validateInstructions.Valparams);

        //         // You may want to return some information to the client indicating that OTP validation is required
        //         return Ok(new
        //         {
        //             RequiresOTPValidation = true,
        //             ValidationInstructions = validateInstructions.Instruction,
        //             FlashMessage = validateInstructions.Valparams
        //         });
        //     }

        //     // Handle other cases or return the chargeResponse as needed
        //     return Ok(chargeResponse);
        // }




    }
}