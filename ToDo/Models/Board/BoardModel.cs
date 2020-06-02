using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDo.Entities;

namespace ToDo.Models.Board
{
    public class BoardModel : PageModel
    {
        public int BoardId { get; set; }
        public string Name { get; set; }
        public string BackgroundPath { get; set; }
        public IEnumerable<Column> Columns { get; set; }
    }
}