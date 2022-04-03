using System.ComponentModel.DataAnnotations;

namespace Homework_22_Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
