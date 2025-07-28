using System;
using Domain.Enum;

namespace Domain.Entity
{
    public class Transaction : BaseEntity
    {
        public string TransactionNum { get; set; }
        public Guid ProduceTypeId { get; set; }
        public ProduceType ProduceType { get; set; }
        public string UnitOfMeasurement { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public Guid FarmerId { get; set; }
        public Farmer Farmer { get; set; }
        public Guid? ManagerId { get; set; }
        public Manager Manager { get; set; }
        // public ICollection<TransactionProduceType> TransactionProduceTypes { get; set; } = new HashSet<TransactionProduceType>();

    }
}