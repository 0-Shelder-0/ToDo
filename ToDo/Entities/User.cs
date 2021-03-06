using System.Collections.Generic;

namespace ToDo.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public virtual IEnumerable<Board> Boards { get; set; }

        public virtual IEnumerable<Thumbnail> Thumbnails { get; set; }
    }
}
