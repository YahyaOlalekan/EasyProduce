using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Product : BaseEntity
    {
        public string ProduceName { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductType> ProductTypes { get; set; } = new HashSet<ProductType>();
        
    }
}
