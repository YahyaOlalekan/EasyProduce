using System;

namespace Domain.Entity
{
    public class CartItem : BaseEntity
    {
        public Guid ProduceTypeId { get; set; }
        public ProduceType ProduceType { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProduceName { get; set; }
        public string NameOfCategory { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}