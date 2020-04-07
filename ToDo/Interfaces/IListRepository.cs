using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IListRepository : IDisposable, IRepository<List>
    {
        IEnumerable<List> GetLists(int boardId);
    }
}
