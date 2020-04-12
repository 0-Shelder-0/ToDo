using System.ComponentModel.DataAnnotations;

namespace ToDo.Models.CreateEntity
{
    public class CreateColumnModel
    {
        [Required(ErrorMessage = "Missing name")]
        public string ColumnName { get; set; }

        public int BoardId { get; set; }
    }
}
