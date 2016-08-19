﻿using System.ComponentModel.DataAnnotations;

namespace CampBusinessLogic.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string Role { get; set; }
    }
}
