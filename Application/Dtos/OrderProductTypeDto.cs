using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class OrderProductTypeDto
    {
        
        public Guid Id { get; set; }
        public Guid ProduceTypeId { get; set; }
        public Guid OrderId { get; set; }
         public string OrderNumber {get;set;}
         public double Quantity {get;set;}
         public decimal Price {get;set;}
    }
}