namespace ToDo.Entities
{
    public class Record : IEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int ColumnId { get; set; }
        public virtual Column Column { get; set; }
    }
}
