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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddAsync([FromForm] CreateRoleRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var role = await _roleService.CreateAsync(model);
            //BaseResponse<RoleDto> role = await _roleService.CreateAsync(model);

            if (role.Status)
            {
                return StatusCode(200, role);
            }
            return StatusCode(400, role);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var role = await _roleService.DeleteAsync(id);
            //  TempData["message"] = role.Message;
            if (role.Status)
            {
                return Ok(role);
            }
            return BadRequest(role);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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


        [Authorize(Roles = "admin")]
        [HttpPut("UpdateRole/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateRoleRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var result = await _roleService.UpdateAsync(id, model);
            // TempData["message"] = result.Message;
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}