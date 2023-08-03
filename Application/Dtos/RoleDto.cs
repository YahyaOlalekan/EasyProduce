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
        [Required, MaxLength(30), MinLength(3)]
        [Display(Name = "Name Of Role")]
        public string RoleName { get; set; }

        [Required, MaxLength(60), MinLength(3)]
        [Display(Name = "Description Of Role")]
        public string RoleDescription { get; set; }
    }
    public class UpdateRoleRequestModel
    {
        [Required, MaxLength(30), MinLength(3)]
         [Display(Name = "Name Of Role")]
        public string RoleName { get; set; }
        
        [Required, MaxLength(60), MinLength(3)]
        [Display(Name = "Description Of Role")]
        public string RoleDescription { get; set; }
    }
}