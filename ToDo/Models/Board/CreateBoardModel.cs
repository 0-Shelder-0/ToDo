using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.Board
{
    public class CreateBoardModel
    {
        [Required (ErrorMessage = "Missing name")]
        public string Name { get; set; }
    }
}
