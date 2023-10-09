using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.Dtos
{
    public class RoleDto
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<UserDto> Users { get; set; }
    }
    public class CreateRoleRequestModel
    {
        [Display(Name = "Name of Role"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string RoleName { get; set; }

        [Display(Name = "Role Description"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Description must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string RoleDescription { get; set; }
    }
    
    public class UpdateRoleRequestModel
    {
        [Display(Name = "Name of Role"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Name must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string RoleName { get; set; }

        [Display(Name = "Role Description"), Required]
        [RegularExpression(@"^[A-Za-z\s'-]*$", ErrorMessage = "Description must not contain numbers")]
        [MaxLength(25, ErrorMessage = "Name should not contain morethan 25 letters"), MinLength(3, ErrorMessage = "Name should not contain lessthan 3 letters")]
        public string RoleDescription { get; set; }
    }
}