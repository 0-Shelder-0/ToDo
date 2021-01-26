using System.Collections.Generic;
using System.Linq;
using ToDo.Data.Interfaces;
using ToDo.Entities;

namespace ToDo.Data.Repositories
{
    public class ImageRepository : EntityRepository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public IEnumerable<Image> GetDefaultImages()
        {
            return DbContext.Images
                            .Where(image => image.IsDefault)
                            .ToList();
        }
    }
}
