using System;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestController : ControllerBase
    {
         private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("AddNewProduceType/{farmerId}")]
        public async Task<IActionResult> AddNewProduceTypeAsync([FromRoute]Guid farmerId, [FromForm] AddNewProduceTypeRequestModel model)
        {
            var request = await _requestService.AddNewProduceTypeAsync(farmerId, model);
            if (ModelState.IsValid)
            {
                if (request != null)
                {
                    return Ok(request);
                }
            }
            return BadRequest(request);
        }
       
       
        [HttpPost("RemoveExistingProduceType/{farmerId}")]
        public async Task<IActionResult> RemoveExistingProduceTypeAsync([FromRoute]Guid farmerId, [FromForm] RemoveExistingProduceTypeRequestModel model)
        {
            var result = await _requestService.RemoveExistingProduceTypeAsync(farmerId, model);
            if (ModelState.IsValid)
            {
                if (result != null)
                {
                    return Ok(result);
                }
            }
            return BadRequest(result);
        }


      

        [HttpPost("GetAllProduceTypeRequests")]
        public async Task<IActionResult> GetAllProduceTypeRequestAsync(GetAllProduceTypeRequestModel model)
        {
            var result = await _requestService.GetAllProduceTypeRequestAsync(model);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }




        [HttpPost("VerifyRequest")]
        public async Task<IActionResult> VerifyRequestAsync(RequestApproveRequestModel model)
        {
            var result = await _requestService.VerifyRequestAsync(model);

            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        
    }
}