using ToDo.Data.Interfaces;
using ToDo.Entities;

namespace ToDo.Data.Repositories
{
    public class ColumnRepository : EntityRepository<Column>, IColumnRepository
    {
        public ColumnRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
