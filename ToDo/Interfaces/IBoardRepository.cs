using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IBoardRepository : IDisposable, IEntityRepository<Board>
    {
        List<Column> GetColumns(int boardId);
    }
}
