using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Category : BaseEntity
    {
        public string NameOfCategory { get; set; }
        public string DescriptionOfCategory { get; set; }
        public ICollection<Produce> Produces { get; set; } = new HashSet<Produce>();
    }
}