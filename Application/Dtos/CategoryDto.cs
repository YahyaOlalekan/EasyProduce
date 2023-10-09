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
        [Display(Name = "Name of category"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string NameOfCategory { get; set; }

        [Display(Name = "Category Description"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Description must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string DescriptionOfCategory { get; set; }
    }
    
    public class UpdateCategoryRequestModel
    {
        [Display(Name = "Name of category"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string NameOfCategory { get; set; }

        [Display(Name = "Category Description"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Description must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string DescriptionOfCategory { get; set; }
    }

}