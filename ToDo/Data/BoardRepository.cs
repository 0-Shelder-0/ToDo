using System;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities;
using ToDo.Interfaces;

namespace ToDo.Data
{
    public class BoardRepository : IBoardRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BoardRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Board GetEntityById(int boardId)
        {
            return _dbContext.Boards.Find(boardId);
        }

        public void InsertEntity(Board board)
        {
            _dbContext.Boards.Add(board);
        }

        public void DeleteEntity(int boardId)
        {
            var board = _dbContext.Boards.Find(boardId);
            _dbContext.Boards.Remove(board);
        }

        public void UpdateEntity(Board board)
        {
            _dbContext.Entry(board).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}