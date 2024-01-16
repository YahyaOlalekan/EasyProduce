using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Transactions;
using Application;
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
        private readonly IFlutterwaveService _flutterwaveService;

        public TransactionController(
            ITransactionService transactionService,
            IPayStackService payStackService,
            ITransactionRepository transactionRepo,
            IFlutterwaveService flutterwaveService
        )
        {
            _transactionService = transactionService;
            _payStackService = payStackService;
            _transactionRepo = transactionRepo;
            _flutterwaveService = flutterwaveService;
        }

        [Authorize(Roles = "farmer")]
        [HttpPut("InitiateProducetypeSales/{farmerId}")]
        public async Task<IActionResult> InitiateProducetypeSalesAsync(
            [FromRoute] Guid farmerId,
            [FromBody] InitiateProducetypeSalesRequestModel model
        )
        {
            if (ModelState.IsValid)
            {
                var result = await _transactionService.InitiateProducetypeSalesAsync(
                    farmerId,
                    model
                );
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
            var initiatedProducetypeSales =
                await _transactionService.GetAllInitiatedProducetypeSalesAsync();
            if (initiatedProducetypeSales == null)
            {
                return NotFound(initiatedProducetypeSales);
            }
            return Ok(initiatedProducetypeSales);
        }

        // [Authorize(Roles = "manager")]
        [HttpPost("VerifyInitiatedProducetypeSales")]
        public async Task<IActionResult> VerifyInitiatedProducetypeSalesAsync(
            InitiatedProducetypeSalesRequestModel model
        )
        {
            var result = await _transactionService.VerifyInitiatedProducetypeSalesAsync(model);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GenerateReceipt/{transactionId}")]
        public async Task<IActionResult> GenerateReceiptAsync(Guid transactionId)
        {
            var transaction = await _transactionService.GenerateReceiptAsync(transactionId);

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }
            return Ok(transaction);
        }

        [Authorize(Roles = "manager")]
        [HttpGet("GetAllConfirmedProducetypeSales")]
        public async Task<IActionResult> GetAllConfirmedProducetypeSalesAsync()
        {
            var confirmedProducetypeSales =
                await _transactionService.GetAllConfirmedProducetypeSalesAsync();
            if (confirmedProducetypeSales == null)
            {
                return NotFound(confirmedProducetypeSales);
            }
            return Ok(confirmedProducetypeSales);
        }

        [HttpPost("InitiatePayoutAsync/{transactionId}")]
        public async Task<IActionResult> InitiatePayoutAsync([FromRoute] Guid transactionId)
        {
            var response = await _flutterwaveService.InitiatePayoutAsync(transactionId);

            if (response.IsSuccessful)
            {
                return Ok(response.OriginalResponse.Content);
            }
            else
            {
                return BadRequest(response.ErrorMessage);
            }
        }

        [HttpGet("GenerateOTPAsync")]
        public async Task<IActionResult> GenerateOTPAsync()
        {
            var otp = await _flutterwaveService.GenerateOTPAsync();

            if (otp == null)
            {
                return NotFound("OTP is not received");
            }
            return Ok(otp);
        }

        [HttpGet("ValidateOtpAsync")]
        public async Task<IActionResult> ValidateOtpAsync(string otp)
        {
            var result = await _flutterwaveService.ValidateOtpAsync(otp);

            if (result == null)
            {
                return BadRequest("OTP is not valid");
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
        public async Task<IActionResult> FinalizePayment(
            [FromRoute] string transferCode,
            string otp
        )
        {
            var result = await _transactionService.MakePaymentAsync(transferCode, otp);

            if (result is not null)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
