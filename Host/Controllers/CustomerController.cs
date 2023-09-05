using System;
using System.Collections.Generic;
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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpPost("CustomerRegistration")]
        public async Task<IActionResult> RegisterAsync([FromForm] CreateCustomerRequestModel model)
        {
            var customer = await _customerService.CreateAsync(model);
            if (!customer.Status)
            {
                return BadRequest(customer);
            }
            return Ok(customer);
        }

        [HttpPut("UpdateCustomer/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateCustomerRequestModel model)
        {
            var customer = await _customerService.UpdateAsync(id, model);
            if (!customer.Status)
            {
                return BadRequest(customer);
            }
            return Ok(customer);
        }

        // [Authorize(Roles = "admin manager")]
        [HttpGet("GetCustomerById/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var customer = await _customerService.GetAsync(id);
            if (!customer.Status)
            {
                return BadRequest(customer);
            }
            return Ok(customer);
        }

        // [Authorize(Roles = "admin")]
        [HttpDelete("DeleteCustomer/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var customer = await _customerService.DeleteAsync(id);
            if (!customer.Status)
            {
                return BadRequest(customer);
            }
            return Ok(customer);
        }

        // [Authorize(Roles = "admin manager")]
        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> ListOfCustomersAsync()
        {
            var customers = await _customerService.GetAllAsync();
            if (customers == null)
            {
                return NotFound(customers);
            }
            return Ok(customers);
        }

    }
}