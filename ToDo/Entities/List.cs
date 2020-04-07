namespace ToDo.Entities
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int BoardId { get; set; }
        public Board Board { get; set; }
    }
}
