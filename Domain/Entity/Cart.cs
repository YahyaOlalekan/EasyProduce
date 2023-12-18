using System;

namespace Domain.Entity
{
    public class Cart : BaseEntity
    {
        public Guid ProduceTypeId { get; set; }
        public ProduceType ProduceType { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProduceName { get; set; } = default!;
        public string NameOfCategory { get; set; } = default!;
        public string UnitOfMeasurement { get; set; } = default!;
        public Guid UserId { get; set; }
        public User User { get; set; }


        //  public Guid ProductTypeId { get; set; }
        // public ProductType ProductType { get; set; }
       
    }
}