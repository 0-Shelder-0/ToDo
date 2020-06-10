using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IThumbnailRepository : IDisposable, IEntityRepository<Thumbnail>
    {
        IEnumerable<Thumbnail> GetThumbnails();
    }
}