using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
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
            var farmer = await _farmerService.CreateAsync(model);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        [HttpPut("UpdateFarmer/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateFarmerRequestModel model)
        {
            var farmer = await _farmerService.UpdateAsync(id, model);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        // [Authorize(Roles = "admin manager")]
        [HttpGet("GetFarmerById{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var farmer = await _farmerService.GetAsync(id);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        // [Authorize(Roles = "admin manager")]
        [HttpDelete("DeleteFarmer/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var farmer = await _farmerService.DeleteAsync(id);
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
            var farmers = await _farmerService.GetAllAsync();
            if (farmers == null)
            {
                return NotFound(farmers);
            }
            return Ok(farmers);
        }

    }
}