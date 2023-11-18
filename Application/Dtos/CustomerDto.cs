using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos
{
    public class CustomerDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Gender Gender { get; set; }
        public string ProfilePicture { get; set; }
        public string Password { get; set; }
        public string RegistrationNumber { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public Guid OrderId { get; set; }

        public List<OrderDto> Orders { get; set; }

    }

    public class CreateCustomerRequestModel
    {
        [Display(Name = "First Name"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string LastName { get; set; }


        [Display(Name = "Phone Number"), Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits")]
        public string PhoneNumber { get; set; }


        [Required, RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter a valid email address!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one letter and one digit.")]
        public string Password { get; set; }


        [Display(Name = "Confirm Password"), Required]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }


        [Required, MinLength(3, ErrorMessage = "Address should not contain lessthan 3 characters")]
        public string Address { get; set; }


        [Required]
        public Gender Gender { get; set; }


        [Display(Name = "Profile Picture"), Required(ErrorMessage = "Please select a profile picture.")]
        public IFormFile ProfilePicture { get; set; }

    }


    public class UpdateCustomerRequestModel
    {
        [Display(Name = "Phone Number"), Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits")]
        public string PhoneNumber { get; set; }

        [Required, MinLength(3, ErrorMessage = "Password should not contain lessthan 3 characters")]
        public string Password { get; set; }

        [Required, MinLength(3, ErrorMessage = "Address should not contain lessthan 3 characters")]
        public string Address { get; set; }

        [Display(Name = "Profile Picture"), Required(ErrorMessage = "Please select a profile picture.")]
        public IFormFile ProfilePicture { get; set; }
    }

}