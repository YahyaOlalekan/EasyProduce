using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entity
{
    public class Farmer : BaseEntity
    {
       
        public string FarmName { get; set; } 
        public string RegistrationNumber { get; set; } 
        public FarmerRegStatus FarmerRegStatus { get; set; } = FarmerRegStatus.Pending;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        
        //public ICollection<Produce>? ProduceToBeSupplying { get; set; } = new HashSet<Produce>();
    }
}