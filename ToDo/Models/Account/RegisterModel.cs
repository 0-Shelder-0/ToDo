using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter correct email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password should not be less than 6 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "The passwords you entered don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
