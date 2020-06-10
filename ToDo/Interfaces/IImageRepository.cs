using System;
using ToDo.Entities;

namespace ToDo.Interfaces
{
    public interface IImageRepository : IDisposable, IEntityRepository<Image> { }
}