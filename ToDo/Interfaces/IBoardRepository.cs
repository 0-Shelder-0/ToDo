using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IBoardRepository : IDisposable, IRepository<Board>
    {
        IEnumerable<Board> GetBoards(int userId);
    }
}
