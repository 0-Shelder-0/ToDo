using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Entities;

namespace ToDo.Models.Board
{
    public class BoardModel : PageModel
    {
        public int BoardId { get; set; }
        public IEnumerable<Record> Records { get; set; }
    }
}
