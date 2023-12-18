using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid ProduceTypeId { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProduceName { get; set; }
        public string NameOfCategory { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Guid UserId { get; set; }

    }

    public class CreateCartRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Produce Type Name")]
        public Guid ProduceTypeId { get; set; }

        [Required]
        public double Quantity { get; set; }
        
        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }

        [Required]
        [Display(Name = "Produce")]
        public Guid ProduceId { get; set; }
        
        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

    }
}
