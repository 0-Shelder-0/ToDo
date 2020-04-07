using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDo.Entities;
using ToDo.Interfaces;
using ToDo.Models.Board;

namespace ToDo.Controllers
{
    public class BoardController : Controller
    {
        private readonly IUserRepository _users;
        private readonly IBoardRepository _boards;
        private readonly IRecordRepository _records;

        public BoardController(IUserRepository users, IBoardRepository boards, IRecordRepository records)
        {
            _users = users;
            _boards = boards;
            _records = records;
        }

        [Authorize]
        public IActionResult Boards(AllBoardsModel model)
        {
            return View();
        }

        [Authorize]
        public IActionResult Board(BoardModel model)
        {
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public IActionResult GetBoards()
        {
            var user = _users.GetUserByEmail(User.Identity.Name);
            var boards = _boards.GetBoards(user.Id);
            return Content("");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateBoard(CreateBoardModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _users.GetUserByEmail(User.Identity.Name);
                var board = new Board {Name = model.Name, User = user, UserId = user.Id};
                _boards.InsertEntity(board);
                _boards.Save();
                var boardModel = new BoardModel {BoardId = board.Id, Records = new List<Record>()};
                return Board(boardModel);
            }
            return RedirectToAction("Boards");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateRecord(CreateRecordModel model)
        {
            return Content("");
        }
    }
}
