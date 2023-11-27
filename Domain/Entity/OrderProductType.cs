
using System;

namespace Domain.Entity
{
    public class OrderProductType : BaseEntity
    {
        // public decimal Price { get; set; }
        // public double Quantity { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }

    }
}