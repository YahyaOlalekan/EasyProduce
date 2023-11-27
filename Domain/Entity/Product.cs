using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<ProductType> ProductTypes { get; set; } = new HashSet<ProductType>();
        
    }
}
