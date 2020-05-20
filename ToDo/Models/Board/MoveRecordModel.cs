namespace ToDo.Models.Board
{
    public class MoveRecordModel
    {
        public int NewColumnId { get; set; }
        public int RecordId { get; set; }
        public int AdjacentRecordId { get; set; }
    }
}