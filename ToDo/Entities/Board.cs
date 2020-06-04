using System.Collections.Generic;

namespace ToDo.Entities
{
    public class Board
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BackgroundNumber { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual List<Column> Columns { get; set; }
    }
}
