using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Application.Dtos.PaymentGatewayDTOs;
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

    }
}