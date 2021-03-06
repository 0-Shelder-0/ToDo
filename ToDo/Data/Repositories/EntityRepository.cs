using System;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities;

namespace ToDo.Data.Repositories
{
    public abstract class EntityRepository<TEntity> where TEntity : class, IEntity
    {
        private protected readonly ApplicationDbContext DbContext;

        protected EntityRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TEntity GetEntity(int entityId)
        {
            return DbContext.Find<TEntity>(entityId);
        }

        public void AddEntity(TEntity entity)
        {
            DbContext.Add(entity);
        }

        public void DeleteEntity(int entityId)
        {
            var entity = GetEntity(entityId);
            DbContext.Remove(entity);
        }

        public void UpdateEntity(TEntity entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            DbContext.SaveChanges();
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
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
