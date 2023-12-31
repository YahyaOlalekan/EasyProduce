
using System;
using Domain.Enum;

namespace Domain.Entity
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Farmer Farmer { get; set; }
        public Customer Customer { get; set; }

    }
}