using System.Collections.Generic;

namespace ToDo.Entities
{
    public class Column
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual List<Record> Records { get; set; }
    }
}
