using System;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities;
using ToDo.Interfaces;

namespace ToDo.Data
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ImageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Image GetEntityById(int entityId)
        {
            return _dbContext.Images.Find(entityId);
        }

        public void InsertEntity(Image image)
        {
            _dbContext.Images.Add(image);
        }

        public void DeleteEntity(int imageId)
        {
            var image = _dbContext.Images.Find(imageId);
            _dbContext.Images.Remove(image);
        }

        public void UpdateEntity(Image image)
        {
            _dbContext.Entry(image).State = EntityState.Modified;
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