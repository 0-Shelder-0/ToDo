using ToDo.Data.Interfaces;
using ToDo.Entities;

namespace ToDo.Data.Repositories
{
    public class RecordRepository : EntityRepository<Record>, IRecordRepository
    {
        public RecordRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
