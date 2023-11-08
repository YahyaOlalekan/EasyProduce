using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Application.Dtos.PaymentGatewayDTOs;
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

        // [HttpGet("testing")]
        // public async Task<IActionResult> Testing([FromQuery] string trxref)
        // {
        //     var model = new VerifyAccountNumberRequestModel
        //     {
        //         AccountNumber = "0159192507",
        //         BankCode = "038",
        //     };
        //     var response = await _payStackService.VerifyAccountNumber(model);


        //     //var response = await _payStackService.ConfirmPayment(trxref);

        //     //var mmod = new InitializeTransactionRequestModel
        //     //{
        //     //    Amount = 2000,
        //     //    Email = "yahyaolalekan2023@gmail.com",
        //     //    RefrenceNo = Guid.NewGuid().ToString(),
        //     //    CallbackUrl = "https://github.com/treehays"
        //     //};
        //     //var response = await _payStackService.InitializePayment(mmod);
        //     return Ok(response);
        // }

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

    }
}