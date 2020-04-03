using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IBoardRepository : IDisposable
    {
        IEnumerable<Board> GetBoards(int userId);
        void InsertBoard(Board board);
        void DeleteBoard(int boardId);
        void UpdateBoard(Board board);
        void Save();
    }
}
