using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IColumnRepository : IDisposable, IRepository<Column>
    {
        IEnumerable<Column> GetColumns(int boardId);
    }
}
