using System;
using System.Collections.Generic;

namespace Domain.Entity
{
    public class Role : BaseEntity
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}