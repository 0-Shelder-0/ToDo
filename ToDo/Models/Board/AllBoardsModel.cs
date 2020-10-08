using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Entities;

namespace ToDo.Models.Board
{
    public class AllBoardsModel : PageModel
    {
        public IEnumerable<ThumbnailBoard> Boards { get; }

        public AllBoardsModel(IEnumerable<Entities.Board> boards)
        {
            Boards = boards.Select(board => new ThumbnailBoard(board));
        }
    }

    public class ThumbnailBoard
    {
        public int Id { get; }
        public string Name { get; }
        public string ThumbnailPath { get; }

        public ThumbnailBoard(Entities.Board board)
        {
            Id = board.Id;
            Name = board.Name;
            ThumbnailPath = board.Image?.Thumbnail?.Path;
        }
    }
}