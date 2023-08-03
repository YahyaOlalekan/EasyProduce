using System;

namespace Domain.Entity
{
    public class Manager : BaseEntity
    {
       
        public string RegistrationNumber { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
  
    }
}