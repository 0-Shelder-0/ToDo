using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.CreateEntity
{
    public class CreateRecordModel
    {
        [Required(ErrorMessage = "Missing name")]
        public string Value { get; set; }
        
        public int BoardId { get; set; }
        public int ColumnId { get; set; }
    }
}
