using System;
using ToDo.Entities;

namespace ToDo.Data.Interfaces
{
    public interface IEntityRepository<TEntity> : IDisposable
        where TEntity : IEntity
    {
        TEntity GetEntity(int entityId);
        void AddEntity(TEntity entity);
        void DeleteEntity(int entityId);
        void UpdateEntity(TEntity entity);
        void Save();
    }
}
