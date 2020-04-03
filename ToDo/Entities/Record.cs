namespace ToDo.Entities
{
    public class Record
    {
        public int Id { get; set; }
        public string Value { get; set; }
        
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
