using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Group : BaseEntity
    {
        public string NameOfGroup { get; set; }
        public string DescriptionOfGroup { get; set; }
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}