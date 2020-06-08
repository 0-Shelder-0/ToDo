using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ToDo.Models.Board
{
    public class AllBoardsModel : PageModel
    {
        public IEnumerable<Entities.Board> Boards { get; set; }
    }
}