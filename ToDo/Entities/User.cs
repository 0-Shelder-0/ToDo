using System.Collections.Generic;

namespace ToDo.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }

        public virtual List<Board> Boards { get; set; }
    }
}
