using System;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduceController : ControllerBase
    {
        private readonly IProduceService _produceService;
        public ProduceController(IProduceService produceService)
        {
            _produceService = produceService;
        }


        [HttpPost("CreateProduce")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProduceRequestModel model)
        {
            var produce = await _produceService.CreateAsync(model);
            if (ModelState.IsValid)
            {
                if (produce.Status)
                {
                    return Ok(produce);
                }
            }
            return BadRequest(produce);
        }


        [HttpDelete("DeleteProduce/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var produce = await _produceService.DeleteAsync(id);
        // TempData["message"] = produce.Message;
            if (produce.Status)
            {
                return Ok(produce);
            }
            return BadRequest(produce);
        }

        [HttpGet("GetProduceDetails/{id}")]
        public async Task<IActionResult> DetailsAsync([FromRoute] Guid id)
        {
            var produce = await _produceService.GetAsync(id);
            if (produce is not null)
            {
                return Ok(produce);

            }
            return NotFound(produce);
        }


        [HttpGet("GetAllProduce")]
        public async Task<IActionResult> ListAsync()
        {
            var produce = await _produceService.GetAllAsync();
            if (produce == null)
            {
                return BadRequest(produce);
            }
            return Ok(produce);
        }
        
        [HttpGet("GetAllProduceByCategoryId/{id}")]
        public async Task<IActionResult> GetAllProduceByCategoryIdAsync([FromRoute] Guid id)
        {
            var produce = await _produceService.GetAllProducesByCategoryIdAsync(id);
            if (produce == null)
            {
                return BadRequest(produce);
            }
            return Ok(produce);
        }


       
        [HttpPut("UpdateProduce/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateProduceRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _produceService.UpdateAsync(id, model);
                // TempData["message"] = result.Message;
                if (result.Status)
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
    }
}