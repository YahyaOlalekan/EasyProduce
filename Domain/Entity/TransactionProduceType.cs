using System;

namespace Domain.Entity
{
    public class TransactionProduceType : BaseEntity
    {
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public Guid ProduceTypeId { get; set; }
        public ProduceType ProduceType { get; set; }
    }
}