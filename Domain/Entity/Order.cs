using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Order : BaseEntity
    {
        public string OrderNumber { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public double TotalQuantity { get; set; }
        public ICollection<OrderProductType> OrderProductTypes { get; set; } = new HashSet<OrderProductType>();
    }
}
