using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController (ITransactionService transactionService) 
        {
            _transactionService = transactionService;
        } 

        [HttpPut("SellProduceType/{farmerId}")]
        public async Task<IActionResult> SellProduceTypeAsync([FromRoute]Guid farmerId, [FromBody]SellProduceTypeRequestModel model)
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