using System.Collections.Generic;
using ToDo.Entities;

namespace ToDo.Data.Interfaces
{
    public interface IImageRepository : IEntityRepository<Image>
    {
        IEnumerable<Image> GetDefaultImages();
    }
}
