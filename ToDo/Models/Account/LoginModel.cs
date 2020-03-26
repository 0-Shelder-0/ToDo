using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter correct email")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Password should not be less than 6 symbols")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
