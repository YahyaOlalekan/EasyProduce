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
        // [Display(Name = "First Name"), Required(ErrorMessage = "First Name is required")]
        // [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        // [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string FirstName { get; set; }
       
       
        // [Display(Name = "Last Name"), Required(ErrorMessage = "Last Name is required")]
        // [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        // [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string LastName { get; set; }

       
        // [Display(Name = "Phone Number"), Required(ErrorMessage = "Phone number is required")]
        // [RegularExpression(@"^\d{11}$", ErrorMessage = "Phone number must be 11 digits")]
        public string PhoneNumber { get; set; }

       
        // ^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$
        // [EmailAddress(ErrorMessage = "Invalid email address")]

        // [Required(ErrorMessage = "Email address is required")]
        // [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Enter a valid email address!")]
        public string Email { get; set; }

       
        // [Required(ErrorMessage = "Password is required"), MinLength(3, ErrorMessage = "Password should not contain lessthan 3 characters")]
        public string Password { get; set; }

       
        // [ Display(Name = "Confirm Password"),Required(ErrorMessage = "Confirm Password is required")]
        // [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

       
        // [Required(ErrorMessage = "Address is required"), MinLength(3, ErrorMessage = "Address should not contain lessthan 3 characters")]
        public string Address { get; set; }

       
        // [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }


        // ^.*\.(jpg|jpeg|png|gif)$
        // [Display(Name = "Profile Picture"), Required(ErrorMessage = "Please select a profile picture.")]
        // [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg)$", ErrorMessage = "Only Image file allowed.")]
        public IFormFile ProfilePicture { get; set; }

    }


    public class UpdateCustomerRequestModel
    {
        // [MaxLength(14), MinLength(11)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, MaxLength(12), MinLength(3)]
        public string Password { get; set; }

        [MaxLength(20), MinLength(3)]
        public string Address { get; set; }

        [Display(Name = "Profile Picture")]
        // [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg)$", ErrorMessage = "Only Image file allowed.")]
        public IFormFile ProfilePicture { get; set; }
    }

}