using ToDo.Data.Interfaces;
using ToDo.Entities;

namespace ToDo.Data.Repositories
{
    public class ImageRepository : EntityRepository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
