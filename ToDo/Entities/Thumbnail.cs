namespace ToDo.Entities
{
    public class Thumbnail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public int ImageId { get; set; }
        public virtual Image Image { get; set; }

        public int? BoardId { get; set; }
        public virtual Board Board { get; set; }
    }
}