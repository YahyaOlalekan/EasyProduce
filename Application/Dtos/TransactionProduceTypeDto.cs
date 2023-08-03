
using System;

namespace Application.Dtos
{
    public class TransactionProduceTypeDto
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
        public Guid TransactionId { get; set; }
        public Guid ProduceTypeId { get; set; }
       
    }
}