using System;
using System.Threading.Tasks;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IRecordRepository : IDisposable, IEntityRepository<Record> { }
}