using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entity
{
    public class Transaction : BaseEntity
    {
        public string TransactionNum {get;set;}
        public Guid FarmerId { get; set; }
        public Farmer Farmer { get; set; }
        public TransactionStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public double TotalQuantity { get; set; }
        public ICollection<TransactionProduceType> TransactionProduceTypes { get; set; } = new HashSet<TransactionProduceType>();
    }
}