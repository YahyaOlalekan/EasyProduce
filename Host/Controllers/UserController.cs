using System;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IJwtAuthenticationManager _tokenService;
        public UserController(IUserService userService, IConfiguration config, IJwtAuthenticationManager tokenService)
        {
            _userService = userService;
            _config = config;
            _tokenService = tokenService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginUserRequestModel model)
        {
            var logging = await _userService.LoginAsync(model);
            if(!logging.Status)
            {
                return BadRequest(logging);
            }
            return Ok(logging);
        }

        [HttpGet("GetUsersById/{id}")]
        public async Task<IActionResult> GetUsersByUserIdAsync([FromRoute]Guid id)
        {
            var user = await _userService.GetAsync(id);
            if(!user.Status)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

          [HttpGet("GetAllUsers")]
        public async Task<IActionResult> ListOfUsersAsync()
        {
            var users = await _userService.GetAllAsync();
            if (users == null)
            {
                return NotFound(users);
            }
            return Ok(users);
        }

        [HttpGet("GetUsersByRole/{role}")]
        public async Task<IActionResult> GetUsersByRoleAsync([FromRoute]string role)
        {
            var users = await _userService.GetAllUsersByRoleAsync(role);
            if(!users.Status)
            {
                return BadRequest(users);
            }
            return Ok(users);
        }

        // [HttpGet("GetUserByToken")]
        // public async Task<IActionResult> GetUserByTokenAsync([FromQuery]string token)
        // {
        //     var user = await _userService.GetUserByTokenAsync(token);
        //     if (!user.Status)
        //     {
        //         return BadRequest(user);
        //     }
        //     return Ok(user);
        // }
    }
}