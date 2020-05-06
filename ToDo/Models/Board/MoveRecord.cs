namespace ToDo.Models.Board
{
    public class MoveRecord
    {
        public int RecordId { get; set; }
        public int NewColumnId { get; set; }

        public int BoardId { get; set; }
    }
}