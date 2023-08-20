using System;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FarmerController : ControllerBase
    {
        private readonly IFarmerService _farmerService;
        public FarmerController(IFarmerService farmerService)
        {
            _farmerService = farmerService;
        }


        [HttpPost("RegisterFarmer")]
        public async Task<IActionResult> RegisterAsync([FromForm] CreateFarmerRequestModel model)
        {
            var farmer = await _farmerService.RegisterFarmerAsync(model);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        [HttpPut("UpdateFarmer/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateFarmerRequestModel model)
        {
            var farmer = await _farmerService.UpdateFarmerAsync(id, model);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        //  [Authorize(Roles = "admin manager")]
        [HttpGet("GetFarmerAlongWithRegisteredProduceType{id}")]
        public async Task<IActionResult> GetFarmerAlongWithRegisteredProduceTypeByIdAsync([FromRoute] Guid id)
        {
            var farmer = await _farmerService.GetFarmerAlongWithRegisteredProduceTypeAsync(id);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        // [Authorize(Roles = "admin")]
        [HttpDelete("DeleteFarmer/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var farmer = await _farmerService.DeleteFarmerAsync(id);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        // [Authorize(Roles = "admin manager")]
        [HttpGet("GetAllFarmers")]
        public async Task<IActionResult> ListOfFarmersAsync()
        {
            var farmers = await _farmerService.GetAllFarmersAsync();
            if (farmers == null)
            {
                return NotFound(farmers);
            }
            return Ok(farmers);
        }

        [HttpGet("GetPendingFarmers")]
        public async Task<IActionResult> PendingFarmersAsync()
        {
            var result = await _farmerService.GetPendingFarmersAsync();
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);

        }


        [HttpPost("VerifyFarmer")]
        public async Task<IActionResult> VerifyAsync(ApproveFarmerDto model)
        {
            var result = await _farmerService.VerifyFarmerAsync(model);

            if (!result.Status)
            {
                return BadRequest(result);
            }

            return Ok(result);

        }

        [HttpGet("ApprovedFarmers")]
        public async Task<IActionResult> ApprovedAsync()
        {
            var result = await _farmerService.GetApprovedFarmersAsync();
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

        [HttpGet("DeclinedFarmers")]
        public async Task<IActionResult> DeclinedAsync()
        {
            var result = await _farmerService.GetDeclinedFarmersAsync();
            if (result == null)
            {
                NotFound(result);
            }

            return Ok(result);
        }



        
    }
}