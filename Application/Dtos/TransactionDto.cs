using System;
using System.Collections.Generic;
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
        public List<TransactionProduceTypeDto> TransactionProduceTypes { get; set; }
        public string TypeName { get; set; }
        public TransactionStatus Status  { get; set; }
        public double TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class CreateTransactionRequestModel
    {
        public List<TransactionDetailsRequestModel> ProduceType { get; set; }
        public string TransactionNum { get; set; }
    }

    public class TransactionDetailsRequestModel
    {

        //[Required]
        //[Display(Name = "Name")]
        //public Guid ProduceName { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public double Quantity { get; set; }

        [Required]
        [Display(Name = "Produce Type Name")]
        public Guid ProduceTypeId { get; set; }

        [Required]
        [Display(Name = "Produce Name")]
        public Guid ProduceId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        //[Required]
        //[Display(Name = "Unit Of Measurement")]
        //public Guid UnitOfMeasurement { get; set; }
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
