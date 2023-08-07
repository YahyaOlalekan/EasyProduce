using System;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddAsync([FromForm] CreateRoleRequestModel model)
        {
            var role = await _roleService.CreateAsync(model);
            //BaseResponse<RoleDto> role = await _roleService.CreateAsync(model);
            if (ModelState.IsValid)
            {
                if (role.Status)
                {
                    return StatusCode(200, role);
                }
            }
            return StatusCode(400, role);
        }


        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> DeleteAsync([FromQuery] Guid id)
        {
            var role = await _roleService.DeleteAsync(id);
        // TempData["message"] = role.Message;
            if (role.Status)
            {
                return Ok(role);
            }
            return BadRequest(role);
        }

        [HttpGet("GetRoleDetails/{id}")]
        public async Task<IActionResult> DetailsAsync([FromRoute] Guid id)
        {
            var role = await _roleService.GetAsync(id);
            if (role is not null)
            {
                return Ok(role);

            }
            return NotFound(role);
        }


        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> ListAsync()
        {
            var roles = await _roleService.GetAllAsync();
            if (roles == null)
            {
                return BadRequest(roles);
            }
            return Ok(roles);
        }


       
        [HttpPut("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateRoleRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.UpdateAsync(id, model);
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