using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entity
{
    public class ProduceType : BaseEntity
    {
        public string TypeName { get; set; }
        public decimal CostPrice { get; set; }
        public string UnitOfMeasurement { get; set; }
       public string TypePicture { get; set; }
        public double TotalQuantityBought { get; set; }

        public ProductType ProductType { get; set; }
        public Guid ProduceId { get; set; }
        public Produce Produce { get; set; }
        public ICollection<FarmerProduceType> FarmerProduceTypes {get;set;} = new HashSet<FarmerProduceType>();
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();

        // public double QuantityToBuy { get; set; }
        //public decimal SellingPrice { get; set; }
        // public TransactionStatus TransactionStatus { get; set; }

        // public ICollection<TransactionProduceType> TransactionProduceTypes { get; set; } = new HashSet<TransactionProduceType>();


        
       
    }
}