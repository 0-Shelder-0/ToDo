using System.Collections.Generic;

namespace ToDo.Entities
{
    public class Image
    {
        public int Id { get; set; }
        public ImageType ImageType { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }

        public int ThumbnailId { get; set; }
        public virtual Thumbnail Thumbnail { get; set; }

        public virtual IEnumerable<Board> Boards { get; set; }
    }
}