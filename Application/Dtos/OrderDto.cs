using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
        public class OrderDto
    {
        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public Guid CustomerId { get; set; }
        public CustomerDto Customer { get; set; }
        public List<OrderProductTypeDto> OrderProductTypes { get; set; }
        public string ProduceName { get; set; }
        public double TotalQuantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class CreateOrderRequestModel
    {
        public List<OrderDetailsRequestModel> ProductType { get; set; }
        public string OrderNumber { get; set; }
    }

    public class OrderDetailsRequestModel
    {

        //[Required]
        //[Display(Name = "Name")]
        //public Guid ProduceName { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public double Quantity { get; set; }
        [Required]
        [Display(Name = "Produce")]
        public Guid ProduceId { get; set; }
        [Required]
        [Display(Name = "Category")]
        public Guid CategoryId { get; set; }
        //[Required]
        //[Display(Name = "Unit Of Measurement")]
        //public Guid UnitOfMeasurement { get; set; }
    }
    public class UpdateOrderRequestModel
    {
        [Required]
        [Display(Name = "Name")]
        public string ProduceName { get; set; }
        // [Required]
        // [Display(Name = "Quantity Available")]
        // public double QuantityAvailabl { get; set; }
        [Required]
        [Display(Name = "Unit Of Measurement")]
        public string UnitOfMeasurement { get; set; }
    }
}