using System;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IColumnRepository : IDisposable, IEntityRepository<Column> { }
}