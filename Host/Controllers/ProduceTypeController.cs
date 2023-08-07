using System;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduceTypeController : ControllerBase
    {
        private readonly IProduceTypeService _produceTypeService;
        public ProduceTypeController(IProduceTypeService produceTypeService)
        {
            _produceTypeService = produceTypeService;
        }


        [HttpPost("CreateProduceType")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProduceTypeRequestModel model)
        {
            var produceType = await _produceTypeService.CreateAsync(model);
            if (ModelState.IsValid)
            {
                if (produceType.Status)
                {
                    return Ok(produceType);
                }
            }
            return BadRequest(produceType);
        }


        [HttpDelete("DeleteProduceType")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var produceType = await _produceTypeService.DeleteAsync(id);
        // TempData["message"] = produce.Message;
            if (produceType.Status)
            {
                return Ok(produceType);
            }
            return BadRequest(produceType);
        }

        [HttpGet("GetProduceTypeDetails/{id}")]
        public async Task<IActionResult> DetailsAsync([FromRoute] Guid id)
        {
            var produceType = await _produceTypeService.GetAsync(id);
            if (produceType is not null)
            {
                return Ok(produceType);

            }
            return NotFound(produceType);
        }


        [HttpGet("GetAllProduceType")]
        public async Task<IActionResult> ListAsync()
        {
            var produceType = await _produceTypeService.GetAllAsync();
            if (produceType == null)
            {
                return BadRequest(produceType);
            }
            return Ok(produceType);
        }


       
        [HttpPut("UpdateProduceType/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateProduceTypeRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _produceTypeService.UpdateAsync(id, model);
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