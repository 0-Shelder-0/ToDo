using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Entities;

namespace ToDo.Models.Board
{
    public class AllBoardsModel : PageModel
    {
        public IEnumerable<ThumbnailBoard> Boards { get; }

        public AllBoardsModel(IEnumerable<ThumbnailBoard> boards)
        {
            Boards = boards;
        }
    }

    public class ThumbnailBoard
    {
        public int Id { get; }
        public string Name { get; }
        public string ThumbnailPath { get; }

        public ThumbnailBoard(int id, string name, string thumbnailPath)
        {
            Id = id;
            Name = name;
            ThumbnailPath = thumbnailPath;
        }
    }
}
