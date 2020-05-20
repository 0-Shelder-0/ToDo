using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ToDo.Entities;
using ToDo.Interfaces;
using ToDo.Models.Board;
using ToDo.Models.CreateEntity;

namespace ToDo.Controllers
{
    public class BoardController : Controller
    {
        private readonly IUserRepository _users;
        private readonly IBoardRepository _boards;
        private readonly IColumnRepository _columns;
        private readonly IRecordRepository _records;

        public BoardController(IUserRepository users, IBoardRepository boards,
                               IColumnRepository columns, IRecordRepository records)
        {
            _users = users;
            _boards = boards;
            _columns = columns;
            _records = records;
        }

        [Authorize]
        [Route("/Board-{id:int}")]
        public IActionResult Board(int id)
        {
            var user = _users.GetUserByEmail(User.Identity.Name);
            var board = user.Boards.FirstOrDefault(b => b.Id == id);
            if (board == null)
                return RedirectToAction("Boards");
            var model = new BoardModel {BoardId = board.Id, Name = board.Name, Columns = board.Columns};
            return View(model);
        }

        [Authorize]
        [Route("Boards")]
        public IActionResult Boards()
        {
            var user = _users.GetUserByEmail(User.Identity.Name);
            var model = new AllBoardsModel {Boards = user.Boards};
            return View(model);
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
                return RedirectToAction("Board", new {id = board.Id});
            }
            ModelState.AddModelError("Name", "Missing name!");
            return RedirectToAction("Boards");
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateColumn(CreateColumnModel model)
        {
            var board = _boards.GetEntityById(model.BoardId);
            if (ModelState.IsValid)
            {
                var column = new Column {Name = model.ColumnName, BoardId = board.Id, Board = board};
                _columns.InsertEntity(column);
                _columns.Save();
            }
            else
            {
                ModelState.AddModelError("Name", "Missing name!");
            }
            return RedirectToAction("Board", new {id = board.Id});
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateRecord(CreateRecordModel model)
        {
            var board = _boards.GetEntityById(model.BoardId);
            if (ModelState.IsValid)
            {
                var column = board.Columns.FirstOrDefault(col => col.Id == model.ColumnId);
                var record = new Record {Column = column, ColumnId = column.Id, Value = model.Value};
                _records.InsertEntity(record);
                _records.Save();
            }
            else
            {
                ModelState.AddModelError("Name", "Missing name!");
            }
            return RedirectToAction("Board", new {id = board.Id});
        }

        [HttpPost]
        [Authorize]
        public IActionResult MoveRecord(MoveRecord model)
        {
            var record = _records.GetEntityById(model.RecordId);
            var adjacentRecord = _records.GetEntityById(model.AdjacentRecordId);
            var column = _columns.GetEntityById(model.NewColumnId);

            record.Column = column;
            record.ColumnId = column.Id;
            _records.UpdateEntity(record);
            _records.Save();

            return Redirect(Request.GetDisplayUrl());
        }
    }
}