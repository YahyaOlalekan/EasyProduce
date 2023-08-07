using System;
using System.ComponentModel.DataAnnotations;
using Domain.Enum;

namespace Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
      public string Token { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string ProfilePicture { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    }
    public class LoginUserRequestModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }

}