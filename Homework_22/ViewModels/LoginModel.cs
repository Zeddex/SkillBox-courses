using System.ComponentModel.DataAnnotations;

namespace Homework_22.ViewModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
