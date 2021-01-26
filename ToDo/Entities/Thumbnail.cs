namespace ToDo.Entities
{
    public class Thumbnail : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsDefault { get; set; }

        public virtual Image Image { get; set; }

        public virtual User User { get; set; }
    }
}
