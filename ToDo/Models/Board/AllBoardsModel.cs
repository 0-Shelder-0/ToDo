using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDo.Models.Board
{
    public class AllBoardsModel : PageModel
    {
        public IEnumerable<MiniBoard> Boards { get; set; }
    }

    public class MiniBoard
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
        public string ThumbnailPath { get; set; }
    }
}