using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Application.Dtos
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public string TransactionNum { get; set; }
        public Guid FarmerId { get; set; }
        public FarmerDto Farmer { get; set; }
        public string TypeName { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid ProduceTypeId { get; set; }

        // public List<TransactionProduceTypeDto> TransactionProduceTypes { get; set; }

    }

    // public class CreateTransactionRequestModel
    // {
    //     public List<TransactionDetailsModel> ProduceTypes { get; set; }
    //     public string TransactionNum { get; set; }
    // }

    // public class TransactionDetailsModel
    // {
    //     [Required]
    //     [Display(Name = "Quantity")]
    //     public double Quantity { get; set; }

    //     [Required]
    //     [Display(Name = "Produce Type Name")]
    //     public Guid ProduceTypeId { get; set; }

    //     [Required]
    //     [Display(Name = "Produce Name")]
    //     public Guid ProduceId { get; set; }

    //     // [Required]
    //     // [Display(Name = "Category")]
    //     // public Guid CategoryId { get; set; }

    //     //[Required]
    //     //[Display(Name = "Unit Of Measurement")]
    //     //public Guid UnitOfMeasurement { get; set; }
    // }

    public class InitiateProducetypeSalesRequestModel/*: BaseEntity*/
    {
        [Display(Name = "Produce Type Name")]
        public Guid ProduceTypeId { get; set; }

        [Required]
        [Display(Name = "Selling Price")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Available Quantity")]
        public double Quantity { get; set; }

        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
        // public decimal TotalAmount { get; set; }
        // public TransactionStatus TransactionStatus { get; set; }

        // public Guid? ManagerId { get; set; }

        // public string TransactionNum { get; set; }
    }
    public class PriceConfirmRequestModel
    {
        [Display(Name = "Produce Type Name")]
        public Guid ProduceTypeId { get; set; }

        [Required]
        [Display(Name = "Selling Price")]
        public decimal Price { get; set; }
    }


    public class UpdateTransactionRequestModel
    {
        [Required]
        [Display(Name = "Name")]
        public string TypeName { get; set; }
        // [Required]
        // [Display(Name = "Quantity Available")]
        // public double QuantityAvailable { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
    }

}
