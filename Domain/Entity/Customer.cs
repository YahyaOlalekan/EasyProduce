using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Customer: BaseEntity
    {
        public string RegistrationNumber { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

    }
}
