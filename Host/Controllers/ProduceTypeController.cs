using System;
using System.Linq;
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var produceType = await _produceTypeService.CreateAsync(model);
            if (produceType.Status)
            {
                return Ok(produceType);
            }
            return BadRequest(produceType);
        }


        [HttpDelete("DeleteProduceType/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
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




        [HttpPost("VerifyProduceType")]
        public async Task<IActionResult> VerifyProduceTypeAsync(ProduceTypeToBeApprovedRequestModel model)
        {
            var result = await _produceTypeService.VerifyProduceTypeAsync(model);

            if (!result.Status)
            {
                return BadRequest(result);
            }
            return Ok(result);
            // return Ok();
        }

       
        [HttpGet("GetProduceTypesToBeApprovedAsync/{farmerId}")]
        public async Task<IActionResult> GetProduceTypesToBeApprovedAsync(Guid farmerId)
        {
            var produceTypes = await _produceTypeService.GetProduceTypesToBeApprovedAsync(farmerId);
            if (produceTypes == null)
            {
                return NotFound(produceTypes);
            }
            return Ok(produceTypes);
        }


        // [HttpGet("GetProduceTypesToBeApprovedAsync")]
        // public async Task<IActionResult> GetProduceTypesToBeApprovedAsync(ProduceTypeToBeApprovedRequestModel model)
        // {
        //     var produceTypes = await _produceTypeService.GetProduceTypesToBeApprovedAsync(model);
        //     if (produceTypes == null)
        //     {
        //         return NotFound(produceTypes);
        //     }
        //     return Ok(produceTypes);
        // }

        [HttpGet("GetApprovedProduceTypesForAFarmerByUserId/{userId}")]
        public async Task<IActionResult> ApprovedProduceTypesForAFarmerAsync([FromRoute] Guid userId)
        {
            var produceTypes = await _produceTypeService.GetAllApprovedProducetypesOfAFarmerAsync(userId);
            if (produceTypes == null)
            {
                return NotFound(produceTypes);
            }
            return Ok(produceTypes);
        }
        [HttpGet("GetApprovedProduceTypesForAFarmerByFarmerId/{farmerId}")]
        public async Task<IActionResult> ApprovedProduceTypesForAFarmerByFarmerIdAsync([FromRoute] Guid farmerId)
        {
            var produceTypes = await _produceTypeService.GetApprovedProduceTypesForAFarmerByFarmerIdAsync(farmerId);
            if (produceTypes == null)
            {
                return NotFound(produceTypes);
            }
            return Ok(produceTypes);
        }

        [HttpGet("GetUnApprovedProduceTypesForAFarmerByUserId/{userId}")]
        public async Task<IActionResult> UnapprovedProduceTypesForAFarmerAsync([FromRoute] Guid userId)
        {
            var produceTypes = await _produceTypeService.GetAllUnapprovedProducetypesOfAFarmerAsync(userId);
            if (produceTypes == null)
            {
                return NotFound(produceTypes);
            }
            return Ok(produceTypes);
        }



        [HttpPut("UpdateProduceType/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateProduceTypeRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var result = await _produceTypeService.UpdateAsync(id, model);
            // TempData["message"] = result.Message;
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}