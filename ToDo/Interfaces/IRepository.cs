using System.Collections.Generic;

namespace ToDo.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> GetEntities();
        void InsertEntity(TEntity entity);
        void DeleteEntity(int entityId);
        void UpdateEntity(TEntity entity);
        void Save();
    }
}
