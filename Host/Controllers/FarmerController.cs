using System;
using System.Linq;
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
    public class FarmerController : ControllerBase
    {
        private readonly IFarmerService _farmerService;
        private readonly IPayStackService _payStackService;
        public FarmerController(IFarmerService farmerService, IPayStackService payStackService)
        {
            _farmerService = farmerService;
            _payStackService = payStackService;
        }


        [HttpPost("RegisterFarmer")]
        public async Task<IActionResult> RegisterAsync([FromForm] CreateFarmerRequestModel model)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var farmer = await _farmerService.UpdateFarmerAsync(id, model);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        //  [Authorize(Roles = "admin manager")]
        [HttpGet("GetFarmerAlongWithRegisteredProduceType/{id}")]
        public async Task<IActionResult> GetFarmerAlongWithRegisteredProduceTypeByIdAsync([FromRoute] Guid id)
        {
            var farmer = await _farmerService.GetFarmerAlongWithRegisteredProduceTypeAsync(id);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        //  [Authorize(Roles = "admin manager")]
        [HttpGet("GetFarmerAlongWithApprovedProduceType/{id}")]
        public async Task<IActionResult> GetFarmerAlongWithApprovedProduceTypeByIdAsync([FromRoute] Guid id)
        {
            var farmer = await _farmerService.GetFarmerAlongWithApprovedProduceTypeAsync(id);
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


        [HttpGet("GetAllBanks")]
        public async Task<IActionResult> GetBanksAsync()
        {
            var banks = await _payStackService.GetBanksAsync();
            if (banks == null)
            {
                return NotFound(banks);
            }
            return Ok(banks);
        }



        [HttpGet("VerifyAccountNumber")]
        public async Task<IActionResult> VerifyAccountNumberAsync([FromQuery] VerifyAccountNumberRequestModel model)
        {
            var result = await _payStackService.VerifyAccountNumber(model);

            if (!result.status)
            {
                return BadRequest(result);
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


        // [HttpPost("GetFarmersByStatus")]
        // public async Task<IActionResult> GetFarmersByStatusAsync(FarmerStatusRequestModel model)
        // {
        //     var result = await _farmerService.GetFarmersByStatusAsync(model);
        //     if (result == null)
        //     {
        //         return NotFound(result);
        //     }
        //     return Ok(result);
        // }


        // [Authorize(Roles = "admin manager")]
        [HttpGet("GetFarmerAccountDetails/{id}")]
        public async Task<IActionResult> GetFarmerAccountDetailsByIdAsync([FromRoute] Guid id)
        {
            var farmer = await _farmerService.GetFarmerAcountDetailsByIdAsync(id);
            if (!farmer.Status)
            {
                return BadRequest(farmer);
            }
            return Ok(farmer);
        }

        //    Console.WriteLine(model.AccountName);
        //    Console.WriteLine(model.AccountNumber);
        //    Console.WriteLine(model.LastName);
        //    Console.WriteLine(model.FirstName);
        //    Console.WriteLine(model.FarmName);
        //    Console.WriteLine(model.Gender);
        //    Console.WriteLine(model.Address);
        //    Console.WriteLine(model.BankCode);
        //    Console.WriteLine(model.Email);
        //    Console.WriteLine(model.Password);
        //    Console.WriteLine(model.PhoneNumber);
        //    Console.WriteLine(model.ProfilePicture);
        //    Console.WriteLine(model.ConfirmPassword);
        //    foreach (var item in model.ProduceTypes)
        //    {
        //      Console.WriteLine(item);
        //    }

    }
}