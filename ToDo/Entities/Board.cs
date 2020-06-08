using System.Collections.Generic;

namespace ToDo.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }

        public virtual IEnumerable<Thumbnail> Thumbnails { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual IEnumerable<Column> Columns { get; set; }
    }
}