using System;
using System.Threading.Tasks;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCategoryRequestModel model)
        {
            var category = await _categoryService.CreateAsync(model);
            if (ModelState.IsValid)
            {
                if (category.Status)
                {
                    return Ok(category);
                }
            }
            return BadRequest(category);
        }


        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var category = await _categoryService.DeleteAsync(id);
        // TempData["message"] = category.Message;
            if (category.Status)
            {
                return Ok(category);
            }
            return BadRequest(category);
        }

        [HttpGet("GetCategoryDetails/{id}")]
        public async Task<IActionResult> DetailsAsync([FromRoute] Guid id)
        {
            var category = await _categoryService.GetAsync(id);
            if (category is not null)
            {
                return Ok(category);

            }
            return NotFound(category);
        }


        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> ListAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories == null)
            {
                return BadRequest(categories);
            }
            return Ok(categories);
        }


       
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateCategoryRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.UpdateAsync(id, model);
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