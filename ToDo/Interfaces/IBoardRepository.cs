using System;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IBoardRepository : IDisposable, IEntityRepository<Board> { }
}