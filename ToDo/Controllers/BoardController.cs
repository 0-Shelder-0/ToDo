using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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
        private const string ImageStorageFolder = "images";
        private const string ThumbnailStorageFolder = "thumbnails";

        private readonly IUserRepository _users;
        private readonly IBoardRepository _boards;
        private readonly IColumnRepository _columns;
        private readonly IRecordRepository _records;
        private readonly IImageRepository _images;
        private readonly IThumbnailRepository _thumbnails;
        private readonly IWebHostEnvironment _appEnvironment;

        public BoardController(IUserRepository users,
                               IBoardRepository boards,
                               IColumnRepository columns,
                               IRecordRepository records,
                               IImageRepository images,
                               IThumbnailRepository thumbnails,
                               IWebHostEnvironment appEnvironment)
        {
            _users = users;
            _boards = boards;
            _columns = columns;
            _records = records;
            _images = images;
            _thumbnails = thumbnails;
            _appEnvironment = appEnvironment;
        }

        [Authorize]
        [Route("/Board-{id:int}")]
        public IActionResult Board(int id)
        {
            var user = _users.GetUserByEmail(User.Identity.Name);
            var board = user.Boards.FirstOrDefault(b => b.Id == id);
            if (board == null)
            {
                return RedirectToAction("Boards");
            }

            var model = new BoardModel
            {
                BoardId = board.Id,
                Name = board.Name,
                Columns = board.Columns,
                BackgroundPath = board.Image?.Path,
                Colors = GetImageModels(board, ImageType.Color),
                Images = GetImageModels(board, ImageType.Image)
            };

            return View(model);
        }

        private IEnumerable<Image> GetImageModels(Board board, ImageType type)
        {
            return board.Thumbnails
                        .Select(thumbnail => thumbnail.Image)
                        .Where(image => image.ImageType == type);
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
                var board = new Board
                {
                    Name = model.Name,
                    User = user,
                    UserId = user.Id
                };
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
                var column = new Column
                {
                    Name = model.ColumnName,
                    BoardId = board.Id,
                    Board = board
                };
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
                var record = new Record
                {
                    Column = column,
                    ColumnId = column.Id,
                    Value = model.Value
                };
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
        public async Task<IActionResult> CreateBackground(CreateBackgroundModel model)
        {
            var board = _boards.GetEntityById(model.BoardId);

            var imagePath = $"{ImageStorageFolder}/{model.File.FileName}";
            await using (var fileStream = new FileStream($"{_appEnvironment.WebRootPath}/{imagePath}", FileMode.Create))
            {
                await model.File.CopyToAsync(fileStream);
            }

            var sourceImage = new KalikoImage(model.File.OpenReadStream());
            var thumbnailName = $"min.{model.File.FileName}";
            var thumbnailPath = $"{ThumbnailStorageFolder}/{thumbnailName}";
            var fitScaling = new FitScaling(192, 108);
            var im = fitScaling.Scale(sourceImage);
            im.SavePng($"{_appEnvironment.WebRootPath}/{thumbnailPath}");

            var image = new Image
            {
                Name = model.File.FileName,
                ImageType = model.Type,
                Path = imagePath,
                Board = board,
            };

            var thumbnail = new Thumbnail
            {
                Name = thumbnailName,
                Path = thumbnailPath,
                Board = board,
                Image = image
            };

            image.Thumbnail = thumbnail;
            thumbnail.Image = image;
            _images.InsertEntity(image);
            _images.Save();

            return RedirectToAction("Board", new {id = model.BoardId});
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeBackground(ChangeBackgroundModel model)
        {
            if (ModelState.IsValid)
            {
                var board = _boards.GetEntityById(model.BoardId);
                if (board == null)
                    return RedirectToAction("Boards");
                var background = _images.GetEntityById(model.ImageId);

                board.Image = background;
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