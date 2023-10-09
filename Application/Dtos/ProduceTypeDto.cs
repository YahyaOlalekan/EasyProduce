using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entity;
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
        public double Quantity { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Status Status { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public Guid CategoryId { get; set; }
        public string DescriptionOfProduce { get; set; }
        public List<TransactionProduceTypeDto> TransactionProduceTypes { get; set; }
        public List<FarmerProduceType> FarmerProduceTypes { get; set; }

    }
    public class CreateProduceTypeRequestModel
    {
        [Display(Name = "Produce Type Name"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string TypeName { get; set; }


        [Display(Name = "Produce"), Required]
        public Guid ProduceId { get; set; }

        // [Required]
        // [Display(Name = "Cost Price")]
        // public decimal CostPrice { get; set; }

        // [Required]
        // [Display(Name = "Selling Price")]
        // public decimal SellingPrice { get; set; }

        // [Required]
        // [Display(Name = "Unit Of Measurement")]
        // public string UnitOfMeasurement { get; set; }

        // [Required]
        // [Display(Name = "Category")]
        // public Guid CategoryId { get; set; }

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

    public class ProduceTypeToBeApprovedRequestModel
    {
        [Display(Name = "Produce Type Name")]
        public Guid ProduceTypeId { get; set; }
        public Guid FarmerId { get; set; }
        public Status Status { get; set; }
    }


    // public class ProduceTypeDetailsToBeSoldByTheFarmerRequestModel
    // {
    //     [Display(Name = "Produce Type Name")]
    //     public Guid Id { get; set; }

    //     // [Display(Name = "Produce Name")]
    //     // public Guid ProduceId { get; set; }

    //     [Required]
    //     [Display(Name = "Selling Price")]
    //     public decimal Price { get; set; }

    //     [Required]
    //     [Display(Name = "Available Quantity")]
    //     public double Quantity { get; set; }

    //     [Required]
    //     [Display(Name = "Unit Of Measurement")]
    //     public string UnitOfMeasurement { get; set; }
    // }

    public class AddToApprovedProduceTypeRequestModel
    {
        [Display(Name = "Produce Type Name")]
        public string TypeName { get; set; }
    }

    public class RemoveFromApprovedProduceTypeRequestModel
    {
        [Display(Name = "Produce Type Name")]
        public string TypeName { get; set; }
    }

    public class UpdateProduceTypeRequestModel
    {
        [Display(Name = "Produce Type Name"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string TypeName { get; set; }

        // [Required]
        // [Display(Name = "Quantity")]
        // public double QuantityToBuy { get; set; }

        // [Required]
        // [Display(Name = "Cost Price")]
        // public decimal CostPrice { get; set; }

        // [Required]
        // [Display(Name = "Selling Price")]
        // public decimal SellingPrice { get; set; }

        // [Required]
        // [Display(Name = "Unit Of Measurement")]
        // public string UnitOfMeasurement { get; set; }

    }
}