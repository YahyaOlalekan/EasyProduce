
using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entity
{
    public class ProductType : BaseEntity
    {
        public string TypeName { get; set; }
        public string TypePicture { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public double QuantityAvailable { get; set; }
        public bool IsAvailable { get; set; }
        public ICollection<OrderProductType> OrderProductTypes { get; set; } = new HashSet<OrderProductType>();

        // public Guid ProduceId { get; set; }
        // public Produce Produce { get; set; }
        // public double QuantityToBuy { get; set; }
        // public Status Status { get; set; } = Status.Pending;


    }
}