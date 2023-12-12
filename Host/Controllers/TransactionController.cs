using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Application.Dtos.PaymentGatewayDTOs;
using Flutterwave.Ravepay.Net;
using Flutterwave.Ravepay.Net.Payments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IPayStackService _payStackService;
        public TransactionController(ITransactionService transactionService, IPayStackService payStackService)
        {
            _transactionService = transactionService;
            _payStackService = payStackService;
        }

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


        // [HttpGet("PaymentTesting/{transactionId}")]
        // public async Task<IActionResult> PaymentTesting([FromRoute] Guid transactionId)
        // {

        //     //Setup the RavePay Config
        //     var publicKey= "FLWPUBK_TEST-423f24968dece0d4bdefedc6c408094d-X";
        //     var raveConfig = new RavePayConfig(publicKey, false);
        //     var accountCharge = new RaveAccountCharge(raveConfig);

        //     var accountParams = new AccountChargeParams(publicKey, "Anonymous", "customer", "user@example.com", "0690000031", 509,
        //      "acessBank.BankCode",  "txRef");

        //     var chargeResponse = await accountCharge.Charge(accountParams);
        //     // Now check the response recieved from the API. Especially the validation status
        //     if (chargeResponse.Data.Status == "success-pending-validation")
        //     {
        //         // This usually means the user needs to validate the transaction with an OTP
        //     }

        //     // if (transferCode is not null)
        //     // {
        //     //     return Ok(transferCode);
        //     // }
        //     // return NotFound(transferCode);
        // }

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

    }
}