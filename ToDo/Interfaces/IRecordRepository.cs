using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IRecordRepository : IDisposable
    {
        IEnumerable<Record> GetRecords(int boardId);
        void InsertRecord(Record record);
        void DeleteRecord(int recordId);
        void UpdateRecord(Record record);
        void Save();
    }
}
