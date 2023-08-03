using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string NameOfCategory { get; set; }
        public string DescriptionOfCategory { get; set; }
        public List<ProduceDto> Produces { get; set; }


    }

    public class CreateCategoryRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
        [Display(Name = "Name of category")]
        public string NameOfCategory { get; set; }

        [Required, MaxLength(60), MinLength(3)]
        [Display(Name = "Description of Category")]
        public string DescriptionOfCategory { get; set; }
    }
    public class UpdateCategoryRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
        [Display(Name = "Name of category")]
        public string NameOfCategory { get; set; }

        [Required, MaxLength(60), MinLength(3)]
        [Display(Name = "Description of Category")]
        public string DescriptionOfCategory { get; set; }
    }

}