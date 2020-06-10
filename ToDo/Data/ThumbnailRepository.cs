using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities;
using ToDo.Interfaces;

namespace ToDo.Data
{
    public class ThumbnailRepository : IThumbnailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ThumbnailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Thumbnail> GetThumbnails()
        {
            return _dbContext.Thumbnails.ToList();
        }

        public Thumbnail GetEntityById(int entityId)
        {
            return _dbContext.Thumbnails.Find(entityId);
        }

        public void InsertEntity(Thumbnail thumbnail)
        {
            _dbContext.Thumbnails.Add(thumbnail);
        }

        public void DeleteEntity(int thumbnailId)
        {
            var thumbnail = _dbContext.Thumbnails.Find(thumbnailId);
            _dbContext.Thumbnails.Remove(thumbnail);
        }

        public void UpdateEntity(Thumbnail thumbnail)
        {
            _dbContext.Entry(thumbnail).State = EntityState.Modified;
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