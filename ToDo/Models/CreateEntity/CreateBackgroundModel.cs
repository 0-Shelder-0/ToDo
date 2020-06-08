using Microsoft.AspNetCore.Http;
using ToDo.Entities;

namespace ToDo.Models.CreateEntity
{
    public class CreateBackgroundModel
    {
        public int BoardId { get; set; }
        public ImageType Type { get; set; }
        public IFormFile File { get; set; }
    }
}