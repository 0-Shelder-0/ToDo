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

        public IEnumerable<Column> GetEntities()
        {
            return _dbContext.Columns.ToList();
        }

        public Column GetEntityById(int entityId)
        {
            return _dbContext.Columns.Find(entityId);
        }

        public void InsertEntity(Column column)
        {
            _dbContext.Columns.Add(column);
        }

        public void DeleteEntity(int columnId)
        {
            var column = _dbContext.Columns.Find(columnId);
            _dbContext.Columns.Remove(column);
        }

        public void UpdateEntity(Column column)
        {
            _dbContext.Entry(column).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public LinkedList<Record> GetRecords(int columnId)
        {
            return _dbContext.Columns.Find(columnId).Records;
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