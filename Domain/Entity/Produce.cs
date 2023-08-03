

using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Produce : BaseEntity
    {
        public string ProduceName { get; set; }
        public string DescriptionOfProduce { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProduceType> ProduceTypes { get; set; } = new HashSet<ProduceType>();

        
    }
}