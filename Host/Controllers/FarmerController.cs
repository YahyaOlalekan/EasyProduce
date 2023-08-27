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


        [HttpPost("GetFarmersByStatus")]
        public async Task<IActionResult> GetFarmersByStatusAsync(FarmerStatusRequestModel model)
        {
            var result = await _farmerService.GetFarmersByStatusAsync(model);
            if (result == null)
            {
                return NotFound(result);
            }
            return Ok(result);
        }


        // [Authorize(Roles = "admin manager")]
        [HttpGet("GetFarmerAccountDetails{id}")]
        public async Task<IActionResult> GetFarmerAccountDetailsByIdAsync([FromRoute] Guid id)
        {
            var farmer = await _farmerService.GetFarmerAcountDetailsByIdAsync(id);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

    }
}