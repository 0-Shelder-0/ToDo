using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities;
using ToDo.Interfaces;

namespace ToDo.Data
{
    public class ColumnRepository : IColumnRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ColumnRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Column> GetColumns(int listId)
        {
            return _dbContext.Lists.Where(list => list.BoardId == listId);
        }

        public IEnumerable<Column> GetEntities()
        {
            return _dbContext.Lists.ToList();
        }

        public void InsertEntity(Column column)
        {
            _dbContext.Lists.Add(column);
        }

        public void DeleteEntity(int listId)
        {
            var list = _dbContext.Lists.Find(listId);
            _dbContext.Lists.Remove(list);
        }

        public void UpdateEntity(Column column)
        {
            _dbContext.Entry(column).State = EntityState.Modified;
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
