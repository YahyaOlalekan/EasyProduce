using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "admin")]
        [HttpPost("CreateProduce")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateProduceRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var produce = await _produceService.CreateAsync(model);
            if (produce.Status)
            {
                return Ok(produce);
            }
            return BadRequest(produce);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteProduce/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var produce = await _produceService.DeleteAsync(id);
            if (produce.Status)
            {
                return Ok(produce);
            }
            return BadRequest(produce);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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


        [Authorize(Roles = "admin")]
        [HttpPut("UpdateProduce/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateProduceRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var result = await _produceService.UpdateAsync(id, model);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}