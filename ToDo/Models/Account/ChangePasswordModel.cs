using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.Account
{
    public class ChangePasswordModel
    {
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [MinLength(6, ErrorMessage = "Password should not be less than 6 symbols")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The passwords you entered don't match")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}