using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Manager : BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Chat> Chats {get;set;} = new HashSet<Chat>();
        public ICollection<Transaction> Transactions {get;set;} = new HashSet<Transaction>();
          
    }
}