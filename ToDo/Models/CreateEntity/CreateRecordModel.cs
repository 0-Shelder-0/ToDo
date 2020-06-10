using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.CreateEntity
{
    public class CreateRecordModel
    {
        [Required(ErrorMessage = "Missing name")]
        public string Value { get; set; }

        [Required]
        public int BoardId { get; set; }

        [Required]
        public int ColumnId { get; set; }
    }
}