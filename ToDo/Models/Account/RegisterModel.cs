using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.Account
{
    public class RegisterModel
    {
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password should not be less than 6 symbols")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords you entered don't match")]
        public string ConfirmPassword { get; set; }
    }
}