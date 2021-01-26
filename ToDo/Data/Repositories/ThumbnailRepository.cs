using System.Collections.Generic;
using System.Linq;
using ToDo.Data.Interfaces;
using ToDo.Entities;

namespace ToDo.Data.Repositories
{
    public class ThumbnailRepository : EntityRepository<Thumbnail>, IThumbnailRepository
    {
        public ThumbnailRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
