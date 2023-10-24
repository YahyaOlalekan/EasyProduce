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

        [HttpGet("testing")]
        public async Task<IActionResult> Testing([FromQuery] string trxref)
        {
            var model = new VerifyAccountNumberRequestModel
            {
                AccountNumber = "0159192507",
                BankCode = "038",
            };
            var response = await _payStackService.VerifyAccountNumber(model);


            //var response = await _payStackService.ConfirmPayment(trxref);

            //var mmod = new InitializeTransactionRequestModel
            //{
            //    Amount = 2000,
            //    Email = "yahyaolalekan2023@gmail.com",
            //    RefrenceNo = Guid.NewGuid().ToString(),
            //    CallbackUrl = "https://github.com/treehays"
            //};
            //var response = await _payStackService.InitializePayment(mmod);
            return Ok(response);
        }

        [HttpPut("SellProduceType/{farmerId}")]
        public async Task<IActionResult> SellProduceTypeAsync([FromRoute] Guid farmerId, [FromBody] SellProduceTypeRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _transactionService.SellProduceType(farmerId, model);
                // TempData["message"] = result.Message;
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }

    }
}