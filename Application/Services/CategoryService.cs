using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpAccessor;

        public CategoryService(ICategoryRepository categoryRepository, IHttpContextAccessor httpAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpAccessor = httpAccessor;
        }

        public async Task<BaseResponse<CategoryDto>> CreateAsync(CreateCategoryRequestModel model)
        {
            // var loginId = _httpAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var categoryExist = await _categoryRepository.GetAsync(a => a.NameOfCategory == model.NameOfCategory);

            if (categoryExist == null)
            {
                var category = new Category();
                category.NameOfCategory = model.NameOfCategory;
                category.DescriptionOfCategory = model.DescriptionOfCategory;
                // category.CreatedBy = loginId;

                string categoryFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.NameOfCategory)}";


                await _categoryRepository.CreateAsync(category);
                await _categoryRepository.SaveAsync();

                return new BaseResponse<CategoryDto>
                {
                    Message = $"Category '{categoryFirstLetterToUpperCase}' Successfully Created",
                    Status = true,
                    Data = null,
                   
                    // Data = new CategoryDto
                    // {
                    //     Id = category.Id,
                    //     NameOfCategory = category.NameOfCategory,
                    //     DescriptionOfCategory = category.DescriptionOfCategory
                    // }
                };
            }

            string categoryExistFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(categoryExist.NameOfCategory)}";

            return new BaseResponse<CategoryDto>
            {
                Message = $"Category '{categoryExistFirstLetterToUpperCase}' Already Exists!",
                Status = false
            };

        }



        public async Task<BaseResponse<CategoryDto>> DeleteAsync(Guid id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category is null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Message = "The category does not exist",
                    Status = false
                };
            }
            category.IsDeleted = true;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveAsync();

         string categoryFirstLetterToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(category.NameOfCategory)}";

            return new BaseResponse<CategoryDto>
            {
             Message = $"Category '{categoryFirstLetterToUpperCase}' Deleted Successfully",
                Status = true
            };

        }


        public async Task<BaseResponse<CategoryDto>> GetAsync(Guid id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category == null)
            {
                return new BaseResponse<CategoryDto>
                {
                    Message = "The Category is Not found!",
                    Status = false,
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Message = "Found",
                Status = true,
                Data = new CategoryDto
                {
                    Id = category.Id,
                    NameOfCategory = category.NameOfCategory,
                    DescriptionOfCategory = category.DescriptionOfCategory
                }
            };
        }



        public async Task<BaseResponse<IEnumerable<CategoryDto>>> GetAllAsync()
        {
            var category = await _categoryRepository.GetAllAsync();
            if (category.Count() == 0)
            {
                return new BaseResponse<IEnumerable<CategoryDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<CategoryDto>>
            {
                Message = "Found",
                Status = true,
                Data = category.Select(c => new CategoryDto
                {
                    Id = c.Id,
                    NameOfCategory = c.NameOfCategory,
                    DescriptionOfCategory = c.DescriptionOfCategory
                })
            };
        }



        public async Task<BaseResponse<CategoryDto>> UpdateAsync(Guid id, UpdateCategoryRequestModel model)
        {
            var category = await _categoryRepository.GetAsync(a => a.Id == id);
            if (category is not null)
            {

                category.NameOfCategory = model.NameOfCategory;
                category.DescriptionOfCategory = model.DescriptionOfCategory;

                _categoryRepository.Update(category);
                await _categoryRepository.SaveAsync();

                return new BaseResponse<CategoryDto>
                {
                    Message = "Category Updated Successfully",
                    Status = true,
                    Data = new CategoryDto
                    {
                        NameOfCategory = category.NameOfCategory,
                        DescriptionOfCategory = category.DescriptionOfCategory,
                        Id = category.Id,
                    }
                };
            }
            return new BaseResponse<CategoryDto>
            {
                Message = "Unable to Update!",
                Status = false,
            };
        }


    }
}