using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.Account
{
    public class ChangePasswordModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password")]
        public string CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password should not be less than 6 symbols")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The passwords you entered don't match")]
        public string ConfirmNewPassword { get; set; }
    }
}