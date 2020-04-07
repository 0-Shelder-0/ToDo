namespace ToDo.Entities
{
    public class Record
    {
        public int Id { get; set; }
        public string Value { get; set; }
        
        public int ListId { get; set; }
        public Board List { get; set; }
    }
}
