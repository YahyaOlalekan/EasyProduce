using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entity
{
    public class ProduceType : BaseEntity
    {
        public string TypeName { get; set; }
        public string TypePicture { get; set; }
        public Guid ProduceId { get; set; }
        public Produce Produce { get; set; }
        public double QuantityToBuy { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Status Status { get; set; } = Status.Pending;
        public ICollection<TransactionProduceType> TransactionProduceTypes { get; set; } = new HashSet<TransactionProduceType>();
        
       
    }
}