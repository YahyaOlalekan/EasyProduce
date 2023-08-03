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


        [HttpPost("DeleteRole")]
        public async Task<IActionResult> DeleteAsync([FromRoute]Guid id)
        {
            var role = await _roleService.DeleteAsync(id);
            //TempData["message"] = role.Message;
            if (role.Status)
            {
                return Ok(role);
            }
            return BadRequest(role);
        }

        [HttpGet("GetRoleDetails")]
        public async Task<IActionResult> DetailsAsync([FromBody]Guid id)
        {
            var role = await _roleService.GetAsync(id);
            if(role is not null)
            {
             return Ok(role);

            }
            return NotFound(role);
        }


        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> ListAsync()
        {
            var roles = await _roleService.GetAllAsync();
            if(roles == null)
            {
                return BadRequest(roles);
            }
            return Ok(roles);
        }


        //  [HttpGet("Update Role")]
        // public async Task<IActionResult> UpdateAsync(Guid id)
        // {
        //    var result = await _roleService.GetAsync(id);
        //    var model = new UpdateRoleRequestModel
        //    {
        //        RoleName = result.Data.RoleName,
        //        RoleDescription = result.Data.RoleDescription,
        //    };
        //     return View(model);
        // }
        // [HttpPost("Update Role")]
        // public IActionResult Update(string id, UpdateRoleRequestModel roleModel)
        // {
        //    if(ModelState.IsValid)
        //    {
        //          var updateRole = _roleService.Update(id, roleModel);
        //         TempData["message"] = updateRole.Message;
        //         if (updateRole.Status)
        //         {
        //             return RedirectToAction("List");
        //         }
        //    }
        //    return View(roleModel);
        // }
    }
}