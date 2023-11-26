using System;
using Domain.Enum;


namespace Domain.Entity;
public class Admin : BaseEntity
{
        public Guid UserId { get; set; }
        public User User { get; set; }
       
}
