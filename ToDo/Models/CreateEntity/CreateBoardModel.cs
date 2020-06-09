using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.CreateEntity
{
    public class CreateBoardModel
    {
        [Required(ErrorMessage = "Missing name")]
        public string Name { get; set; }
    }
}