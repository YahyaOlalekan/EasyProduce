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
        private readonly IMailService _configMailService;
        public UserController(IUserService userService, IConfiguration config, IJwtAuthenticationManager tokenService, IMailService configMailService)
        {
            _userService = userService;
            _config = config;
            _tokenService = tokenService;
            _configMailService = configMailService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromForm]LoginUserRequestModel model)
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
            // var mmm = new MailRequest{
            //   ToEmail = "yahyaolalekan2023@gmail.com",
            //   ToName = "ade",
            //   AttachmentName  = "sola",
            //   HtmlContent = "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"><title>Document</title></head><body><h4>Hello, welcome on board</h4></body></html>",
            //   Subject = "status of registration",
            // };
            //  var mmm = new EmailSenderDetails{
            //     ReceiverName ="yahhh",
            //     ReceiverEmail = "yahyaolalekan2023@gmail.com",
            //     EmailToken = "yafytfta",

            //  };

            // await _configMailService.EmailVerificationTemplate(mmm,"uygguygyytf");
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