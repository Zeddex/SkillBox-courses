using System.ComponentModel.DataAnnotations;

namespace Homework_22.ViewModels
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Please enter login")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        public string Password { get; set; }
    }
}
