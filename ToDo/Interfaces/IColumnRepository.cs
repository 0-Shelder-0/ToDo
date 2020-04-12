using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IColumnRepository : IDisposable, IEntityRepository<Column>
    {
        List<Record> GetRecords(int columnId);
    }
}
