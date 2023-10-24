using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

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
            if (!ModelState.IsValid)
            {
                // Model validation failed; collect errors
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();

                // Return validation errors with a 400 Bad Request status code
                return BadRequest(errors);
            }

            var customer = await _customerService.UpdateAsync(id, model);
            if (!customer.Status)
            {
                return BadRequest(customer);
            }
            return Ok(customer);
        }

        // [Authorize]
        [HttpGet("GetCustomerById/{id?}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid? id)
        {
            // var ddd = Request.Headers;
            if (id is null)
            {
                id = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }

            var customer = await _customerService.GetAsync(id.Value);
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