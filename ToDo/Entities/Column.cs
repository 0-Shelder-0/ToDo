using System.Collections.Generic;

namespace ToDo.Entities
{
    public class Column : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int BoardId { get; set; }
        public virtual Board Board { get; set; }

        public virtual IEnumerable<Record> Records { get; set; }
    }
}
