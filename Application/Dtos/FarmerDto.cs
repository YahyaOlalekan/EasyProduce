using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Entity;
using Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos
{
    public class FarmerDto
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
        public string? FarmName { get; set; }
        public FarmerRegStatus FarmerRegStatus { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
        public Guid TransactionId { get; set; }
        public List<TransactionDto> Transactions { get; set; }

        public string BankName { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }

        public ICollection<Chat> Chats { get; set; } = new HashSet<Chat>();
        // public ICollection<FarmerProduceType> FarmerProduceTypes { get; set; } = new HashSet<FarmerProduceType>();

    }


    public class CreateFarmerRequestModel
    {
        public List<string> ProduceTypes { get; set; }

        [Required, MaxLength(20), MinLength(3)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(20), MinLength(3)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, MaxLength(14), MinLength(11)]
        [Display(Name = "Phone Number")]
        // [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; }

        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$",
        ErrorMessage = "Enter a valid email address!")]
        //[EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(12), MinLength(3)]
        // [DataType(DataType.Password)]
        public string Password { get; set; }

        // [DataType(DataType.Password)]
        [Required, MaxLength(12), MinLength(3), Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required, MaxLength(20), MinLength(3)]
        public string Address { get; set; }

        public Gender Gender { get; set; }

        public string? FarmName { get; set; }

        public string BankName { get; set; }
        public string AccountName { get; set; }
        public int AccountNumber { get; set; }

        [Display(Name = "Profile Picture"), Required(ErrorMessage = "Please select file.")]
        // [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jpeg)$", ErrorMessage = "Only Image file allowed.")]
        public IFormFile ProfilePicture { get; set; }


    }


    public class UpdateFarmerRequestModel
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

    public class ApproveFarmerDto
    {
        public Guid Id { get; set; }
        public FarmerRegStatus Status { get; set; }
    }
    public class FarmerStatusRequestModel
    {
        public FarmerRegStatus Status { get; set; }
    }
}