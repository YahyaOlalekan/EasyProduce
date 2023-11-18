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
        public List<Guid> ProduceTypeIds { get; set; }

        // public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }

        public ICollection<Chat> Chats { get; set; } = new HashSet<Chat>();
        // public ICollection<FarmerProduceType> FarmerProduceTypes { get; set; } = new HashSet<FarmerProduceType>();

    }


    public class CreateFarmerRequestModel
    {
        [Display(Name = "Select Produce Types"), Required(ErrorMessage = "You must select atleast one produce type")]
        public List<Guid> ProduceTypes { get; set; }

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


        [Display(Name = "Farm Name")]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 50 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string? FarmName { get; set; }


        [Display(Name = "Bank Name"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 30 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string BankName { get; set; }

        [Display(Name = "Bank Code"), Required]
        //[RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        //[MaxLength(25, ErrorMessage = "Name should not contain morethan 30 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string BankCode { get; set; }


        [Display(Name = "AccountName"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 50 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string AccountName { get; set; }


        [Display(Name = "Account Number"), Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account Number must be 10 digits")]
        public string AccountNumber { get; set; }

    }


    public class UpdateFarmerRequestModel
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