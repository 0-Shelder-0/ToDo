using System;
using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IImageRepository : IDisposable, IEntityRepository<Image>
    {
        IEnumerable<Image> GetImages(ImageType type);
    }
}