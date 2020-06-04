using System.Collections.Generic;

namespace ToDo.Interfaces
{
    public interface IImageKeeper
    {
        public List<string> ImagePaths { get; }
        public List<string> ThumbnailImagePaths { get; }
    }
}