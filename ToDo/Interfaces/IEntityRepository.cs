using System.Collections.Generic;

namespace ToDo.Interfaces
{
    public interface IEntityRepository<TEntity>
    {
        IEnumerable<TEntity> GetEntities();
        TEntity GetEntityById(int entityId);
        void InsertEntity(TEntity entity);
        void DeleteEntity(int entityId);
        void UpdateEntity(TEntity entity);
        void Save();
    }
}
