using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IRecordRepository : IDisposable, IRepository<Record>
    {
        IEnumerable<Record> GetRecords(int listId);
    }
}
