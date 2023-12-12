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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCategoryRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var category = await _categoryService.CreateAsync(model);

            if (category.Status)
            {
                return Ok(category);
            }
            
            return BadRequest(category);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var category = await _categoryService.DeleteAsync(id);
            if (category.Status)
            {
                return Ok(category);
            }
            return BadRequest(category);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
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


        [Authorize(Roles = "admin")]
        [HttpPut("UpdateCategory/{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateCategoryRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();
                return BadRequest(errors);
            }

            var result = await _categoryService.UpdateAsync(id, model);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}