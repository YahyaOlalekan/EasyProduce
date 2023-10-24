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
    public class ChatController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IJwtAuthenticationManager _tokenService;
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService, IJwtAuthenticationManager tokenService, IConfiguration config)
        {
            _chatService = chatService;
            _tokenService = tokenService;
            _config = config;
        }
        
        [HttpPost("CreateChat/{id}/{farmerId}")]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatRequestModel model, [FromRoute] Guid id, [FromRoute] Guid farmerId)
        {
            // string token = Request.Headers["Authorization"];
            // string extractedToken = token.Substring(7);
            // var isValid = _tokenService.IsTokenValid(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), extractedToken);
            // if (!isValid)
            // {
            //     return Unauthorized();
            // }
            var chat = await _chatService.CreateChat(model, id, farmerId);
            if (!chat.Status)
            {
                return BadRequest(chat);
            }
            return Ok(chat);
        }


        [HttpGet("Get/{managerId}/{farmerId}")]
        public async Task<IActionResult> GetChatFromASenderAsync([FromRoute] Guid managerId, [FromRoute] Guid farmerId)
        {
            var chat = await _chatService.GetChatFromASenderAsync(managerId, farmerId);
            if (!chat.Status)
            {
                return BadRequest(chat);
            }
            return Ok(chat);
        }

        // [HttpPut("MarkAllAsRead/{managerId}/{farmerId}")]
        // public async Task<IActionResult> MarkAllAsRead([FromRoute]Guid managerId,[FromRoute] Guid farmerId)
        // {
        //     var response = await _chatService.MarkAllChatsAsReadAsync(managerId,farmerId);
        //     if (!response.Status)
        //     {
        //         return BadRequest(response);
        //     }
        //     return Ok(response);
        // }
        // [HttpGet("GetAllUnSeenChatAsync/{farmerId}")]
        // public async Task<IActionResult> GetAllUnseenChats([FromRoute] Guid farmerId)
        // {
        //     var response = await _chatService.GetAllUnSeenChatAsync(farmerId);
        //      if (!response.Status)
        //     {
        //         return BadRequest(response);
        //     }
        //     return Ok(response);
        // }
    }
}