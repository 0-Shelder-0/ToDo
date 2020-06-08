using System;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IThumbnailRepository : IDisposable, IEntityRepository<Thumbnail> { }
}