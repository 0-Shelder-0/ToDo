using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Data.Interfaces
{
    public interface IThumbnailRepository : IEntityRepository<Thumbnail>
    {
        IEnumerable<Thumbnail> GetDefaultThumbnails();
    }
}
