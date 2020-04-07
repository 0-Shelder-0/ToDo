using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities;
using ToDo.Interfaces;

namespace ToDo.Data
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RecordRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Record> GetRecords(int listId)
        {
            return _dbContext.Records.Where(record => record.ListId == listId);
        }

        public IEnumerable<Record> GetEntities()
        {
            return _dbContext.Records.ToList();
        }

        public void InsertEntity(Record record)
        {
            _dbContext.Records.Add(record);
        }

        public void DeleteEntity(int recordId)
        {
            var record = _dbContext.Records.Find(recordId);
            _dbContext.Records.Remove(record);
        }

        public void UpdateEntity(Record record)
        {
            _dbContext.Entry(record).State = EntityState.Modified;
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
