using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Application.Dtos
{
    public class ProductTypeDto
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
       // public IFormFile TypePicture { get; set; }
        public Guid ProduceId { get; set; }
        public double QuantityToBuy { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public ProduceTypeDto ProduceType { get; set; }
        public bool IsAvailable { get; set; }
        public Status Status { get; set; } 
        public List<OrderProductTypeDto> OrderProductTypes { get; set; }
        public Guid CategoryId { get; set; }
        public double QuantityToSell { get; set; }
       
    }

    public class CreateProductTypeRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public Guid ProduceTypeId { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal SellingPrice { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public double QuantityToSell { get; set; }

        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        
        [Required]
        [Display(Name = "Produce Name")]
        public Guid ProduceId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        // public IFormFile TypePicture { get; set; }

    }


    public class SellProductTypeRequestModel
    {

        [Required, MinLength(1), MaxLength(50)]
        [Display(Name = "Produce Type Name")]
        public List<Guid> ProductTypeId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public List<string> TypeName { get; set; }


        [Required]
        [Display(Name = "Quantity")]
        public List<double> Quantity { get; set; }

        // public SelectList CategoryList { get; set; }

        // [Required]
        // [Display(Name = "Name of Category")]
        // public Guid NameOfCategory { get; set; }
    }

    public class UpdateProductTypeRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Name")]
        public string TypeName { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public double QuantityToSell { get; set; }
        [Required]
        [Display(Name = "Price")]
        public decimal SellingPrice { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        // [Required]
        // [Display(Name = "Name of Category")]
        // public Guid NameOfCategory { get; set; }
    }
}