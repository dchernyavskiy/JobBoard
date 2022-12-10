﻿using System.ComponentModel.DataAnnotations;

namespace JobBoard.Identity.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Email { get; set; }

        //[Required]
        //public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }
        public string Role { get; set; } = "Employee";
    }
}