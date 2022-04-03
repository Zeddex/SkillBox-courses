﻿using System.ComponentModel.DataAnnotations;

namespace Homework_22_Web.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Please enter login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }
}
