using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }


        [HttpPost("RegisterManager")]
        public async Task<IActionResult> RegisterAsync([FromForm] CreateManagerRequestModel model)
        {
            var manager = await _managerService.CreateAsync(model);
            if (!manager.Status)
            {
                return BadRequest(manager);
            }
            return Ok(manager);
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateManagerRequestModel model)
        {
            var manager = await _managerService.UpdateAsync(id, model);
            if (!manager.Status)
            {
                return BadRequest(manager);
            }
            return Ok(manager);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var manager = await _managerService.GetAsync(id);
            if (!manager.Status)
            {
                return BadRequest(manager);
            }
            return Ok(manager);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var manager = await _managerService.DeleteAsync(id);
            if (!manager.Status)
            {
                return BadRequest(manager);
            }
            return Ok(manager);
        }

        [HttpGet("GetAllManagers")]
        public async Task<IActionResult> ListOfManagersAsync()
        {
            var managers = await _managerService.GetAllAsync();
            if (managers == null)
            {
                return NotFound(managers);
            }
            return Ok(managers);
        }

    }
}