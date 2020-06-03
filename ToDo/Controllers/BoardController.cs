using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ToDo.Entities;
using ToDo.Interfaces;
using ToDo.Models.Board;
using ToDo.Models.CreateEntity;
using ToDo.Models.RemoveEntity;

namespace ToDo.Controllers
{
    public class BoardController : Controller
    {
        private readonly IUserRepository _users;
        private readonly IBoardRepository _boards;
        private readonly IColumnRepository _columns;
        private readonly IRecordRepository _records;
        private readonly IImageKeeper _imageKeeper;

        public BoardController(IUserRepository users, IBoardRepository boards, IColumnRepository columns,
                               IRecordRepository records, IImageKeeper imageKeeper)
        {
            _users = users;
            _boards = boards;
            _columns = columns;
            _records = records;
            _imageKeeper = imageKeeper;
        }

        [Authorize]
        [Route("/Board-{id:int}")]
        public IActionResult Board(int id)
        {
            var user = _users.GetUserByEmail(User.Identity.Name);
            var board = user.Boards.FirstOrDefault(b => b.Id == id);
            if (board == null)
                return RedirectToAction("Boards");

            var domain = $"{Request.Scheme}://{Request.Host}/";
            var model = new BoardModel
            {
                BoardId = board.Id,
                Name = board.Name,
                Columns = board.Columns,
                BackgroundPath = domain + _imageKeeper.ImagePaths[board.BackgroundNumber],
                ThumbnailPaths = _imageKeeper.ThumbnailImagePaths.Select(path => domain + path)
            };
            return View(model);
        }

        [Authorize]
        [Route("Boards")]
        public IActionResult Boards()
        {
            var user = _users.GetUserByEmail(User.Identity.Name);
            var model = new AllBoardsModel
            {
                Boards = user.Boards.Select(board => new MiniBoard
                {
                    Name = board.Name,
                    BoardId = board.Id,
                    ThumbnailPath = _imageKeeper.ThumbnailImagePaths[board.BackgroundNumber]
                })
            };
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
        public IActionResult MoveRecord(MoveRecordModel model)
        {
            var record = _records.GetEntityById(model.RecordId);
            var column = _columns.GetEntityById(model.NewColumnId);

            record.Column = column;
            record.ColumnId = column.Id;
            _records.UpdateEntity(record);
            _records.Save();

            return RedirectToAction("Board", new {id = column.BoardId});
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeBackground(ChangeBackgroundModel model)
        {
            if (ModelState.IsValid)
            {
                var board = _boards.GetEntityById(model.BoardId);
                var number = 0;
                if (model.NewBackgroundPath != null)
                {
                    number = _imageKeeper.ThumbnailImagePaths
                                         .FindIndex(path => path.Contains(model.NewBackgroundPath.Split('/').Last()));
                }

                board.BackgroundNumber = number;
                _boards.UpdateEntity(board);
                _boards.Save();
            }

            return RedirectToAction("Board", new {id = model.BoardId});
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveRecord(RemoveRecordModel model)
        {
            return RemoveEntity(_records, model.RecordId, model.BoardId);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveColumn(RemoveColumnModel model)
        {
            return RemoveEntity(_columns, model.ColumnId, model.BoardId);
        }

        [HttpPost]
        [Authorize]
        public IActionResult RemoveBoard(int boardId)
        {
            _boards.DeleteEntity(boardId);
            _boards.Save();

            return RedirectToAction("Boards");
        }

        private IActionResult RemoveEntity<T>(IEntityRepository<T> repository, int id, int boardId)
        {
            repository.DeleteEntity(id);
            repository.Save();

            return RedirectToAction("Board", new {id = boardId});
        }
    }
}