using ToDo.Data.Interfaces;
using ToDo.Entities;

namespace ToDo.Data.Repositories
{
    public class BoardRepository : EntityRepository<Board>, IBoardRepository
    {
        public BoardRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
