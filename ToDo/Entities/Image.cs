namespace ToDo.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public ImageType ImageType { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public virtual Thumbnail Thumbnail { get; set; }

        public int? BoardId { get; set; }
        public virtual Board Board { get; set; }
    }
}