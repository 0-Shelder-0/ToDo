using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.Board
{
    public class ChangeEntityModel
    {
        [Required]
        public int BoardId { get; set; }

        [Required]
        public int EntityId { get; set; }

        [Required]
        public string Value { get; set; }
    }
}