using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Entities;

namespace ToDo.Models.Board
{
    public class BoardModel : PageModel
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
        public string BackgroundPath { get; set; }

        public IEnumerable<ImageModel> Colors { get; set; }
        public IEnumerable<ImageModel> Images { get; set; }

        public IEnumerable<ColumnModel> Columns { get; set; }
    }

    public class ImageModel
    {
        public int ThumbnailId { get; set; }
        public string ThumbnailPath { get; set; }

        public ImageModel(int thumbnailId, string thumbnailPath)
        {
            ThumbnailId = thumbnailId;
            ThumbnailPath = thumbnailPath;
        }
    }

    public class ColumnModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<RecordModel> Records { get; set; }

        public ColumnModel(int id, string name, IEnumerable<Record> records)
        {
            Id = id;
            Name = name;
            Records = records.Select(record => new RecordModel(record.Id, record.Value));
        }
    }

    public class RecordModel
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public RecordModel(int id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}
