using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IColumnRepository : IDisposable, IEntityRepository<Column>
    {
        LinkedList<Record> GetRecords(int columnId);
    }
}
