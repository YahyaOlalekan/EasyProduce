using System;
using System.Collections.Generic;
using Domain.Enum;

namespace Domain.Entity
{
    public class Farmer : BaseEntity
    {

        public string? FarmName { get; set; }
        public string RegistrationNumber { get; set; }
        public FarmerRegStatus FarmerRegStatus { get; set; } = FarmerRegStatus.Pending;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        public ICollection<Chat> Chats { get; set; } = new HashSet<Chat>();
        public ICollection<Request> Requests { get; set; } = new HashSet<Request>();
        public ICollection<FarmerProduceType> FarmerProduceTypes { get; set; } = new HashSet<FarmerProduceType>();

    }
}