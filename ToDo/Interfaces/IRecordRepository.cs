using System;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IRecordRepository : IDisposable, IEntityRepository<Record> { }
}