using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Application.Dtos
{
    public class ProduceTypeDto
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
       // public string TypePicture { get; set; }
        public Guid ProduceId { get; set; }
        public string ProduceName { get; set; }
        public string NameOfCategory { get; set; }
        public double QuantityToBuy { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Status Status { get; set; }
        public List<TransactionProduceTypeDto> TransactionProduceTypes { get; set; }
        public Guid CategoryId { get; set; }
        public string DescriptionOfProduce { get; set; }

    }
    public class CreateProduceTypeRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Produce Type Name")]
        public string TypeName { get; set; }

        [Required]
        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

        [Required]
        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }

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


    public class PurchaseProduceTypeRequestModel
    {
        // [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Produce Name")]
        public Guid ProduceId { get; set; }

        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }

        // public SelectList CategoryList { get; set; }

        [Required]
        [Display(Name = "Produce Type Name")]
        public List<Guid> ProduceTypeId { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public List<double> QuantityToBuy { get; set; }

        // public Guid UnitOfMeasurement { get; set; }
    }


    public class UpdateProduceTypeRequestModel
    {
        [Required, MinLength(3), MaxLength(50)]
        [Display(Name = "Produce Type Name")]
        public string TypeName { get; set; }

        [Required]
        [Display(Name = "Quantity")]
        public double QuantityToBuy { get; set; }

        [Required]
        [Display(Name = "Cost Price")]
        public decimal CostPrice { get; set; }

        [Required]
        [Display(Name = "Selling Price")]
        public decimal SellingPrice { get; set; }

        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        
    }
}