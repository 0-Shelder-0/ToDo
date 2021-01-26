using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kaliko.ImageLibrary;
using Kaliko.ImageLibrary.Scaling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ToDo.Data.Interfaces;
using ToDo.Entities;
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
        private readonly IWebHostEnvironment _appEnvironment;

        public BoardController(IUserRepository users,
                               IBoardRepository boards,
                               IColumnRepository columns,
                               IRecordRepository records,
                               IImageRepository images,
                               IWebHostEnvironment appEnvironment)
        {
            _users = users;
            _boards = boards;
            _columns = columns;
            _records = records;
            _images = images;
            _appEnvironment = appEnvironment;
        }

        [Authorize]
        [Route("boards/{id:int}")]
        public IActionResult Board(int id)
        {
            var user = _users.GetUserByEmail(User.Identity.Name);
            var board = user.Boards.FirstOrDefault(b => b.Id == id);

            if (board == null)
            {
                return RedirectToAction("Boards");
            }
            var boardModel = new BoardModel
            {
                BoardId = board.Id,
                Name = board.Name,
                Columns = board.Columns.Select(column => new ColumnModel(column.Id, column.Name, column.Records)),
                BackgroundPath = board.Image?.Path,
                Colors = GetImages(user, ImageType.Color),
                Images = GetImages(user, ImageType.Image)
            };
            return View(boardModel);
        }

        private IEnumerable<ImageModel> GetImages(User user, ImageType type)
        {
            return _images.GetDefaultImages()
                          .Concat(GetUserImages(user))
                          .Where(image => image.ImageType == type)
                          .Select(image => new ImageModel(image.ThumbnailId, image.Thumbnail.Path));
        }

        private IEnumerable<Image> GetUserImages(User user)
        {
            return user.Thumbnails.Select(thumbnail => thumbnail.Image);
        }

        [Authorize]
        [Route("boards")]
        public IActionResult Boards()
        {
            var user = _users.GetUserByEmail(User.Identity.Name);
            var model = new AllBoardsModel(
                user.Boards
                    .ToList()
                    .Select(board => new ThumbnailBoard(board.Id, board.Name, board.Image?.Thumbnail?.Path))
                    .ToList());

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
                    Name = model.Name.Trim(),
                    User = user,
                };

                _boards.AddEntity(board);
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
            var board = _boards.GetEntity(model.BoardId);
            if (board == null)
            {
                return RedirectToAction("Boards");
            }
            if (ModelState.IsValid)
            {
                var column = new Column
                {
                    Name = model.ColumnName.Trim(),
                    Board = board
                };
                _columns.AddEntity(column);
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
            var board = _boards.GetEntity(model.BoardId);
            if (board == null)
            {
                return RedirectToAction("Boards");
            }
            if (ModelState.IsValid)
            {
                var column = board.Columns.FirstOrDefault(col => col.Id == model.ColumnId);
                var record = new Record
                {
                    Column = column,
                    Value = model.Value.Trim()
                };
                _records.AddEntity(record);
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
        [Route("boards/moveRecord")]
        public IActionResult MoveRecord(MoveRecordModel model)
        {
            var record = _records.GetEntity(model.RecordId);
            var column = _columns.GetEntity(model.NewColumnId);

            if (record != null && column != null)
            {
                record.Column = column;

                _records.UpdateEntity(record);
                _records.Save();
            }
            return RedirectToAction("Board", new {id = column.BoardId});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateBackground(CreateBackgroundModel model)
        {
            var board = _boards.GetEntity(model.BoardId);
            if (board == null)
            {
                return RedirectToAction("Boards");
            }
            var imagePath = $"/{ImageStorageFolder}/{model.File.FileName}";
            await SaveImageAsync(model, imagePath);

            var thumbnailName = $"min.{model.File.FileName}";
            var thumbnailPath = $"/{ThumbnailStorageFolder}/{thumbnailName}";
            SaveThumbnail(model, thumbnailPath);
            var image = new Image
            {
                Name = model.File.FileName,
                ImageType = model.Type,
                Path = imagePath,
                Boards = new List<Board> {board}
            };
            var thumbnail = new Thumbnail
            {
                Name = thumbnailName,
                Path = thumbnailPath,
                User = board.User,
                Image = image
            };
            image.Thumbnail = thumbnail;
            _images.AddEntity(image);
            _images.Save();
            return RedirectToAction("Board", new {id = model.BoardId});
        }

        private void SaveThumbnail(CreateBackgroundModel model, string thumbnailPath)
        {
            var sourceImage = new KalikoImage(model.File.OpenReadStream());
            var fitScaling = new FitScaling(192, 108);
            var image = fitScaling.Scale(sourceImage);
            image.SavePng($"{_appEnvironment.WebRootPath}{thumbnailPath}");
        }

        private async Task SaveImageAsync(CreateBackgroundModel model, string imagePath)
        {
            var path = $"{_appEnvironment.WebRootPath}{imagePath}";
            await using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await model.File.CopyToAsync(fileStream);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeBackground(ChangeBackgroundModel model)
        {
            var board = _boards.GetEntity(model.BoardId);
            var background = _images.GetEntity(model.ImageId);
            if (board == null || background == null)
            {
                return RedirectToAction("Boards");
            }
            board.Image = background;
            _boards.UpdateEntity(board);
            _boards.Save();

            return RedirectToAction("Board", new {id = model.BoardId});
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeColumnName(ChangeEntityModel model)
        {
            if (ModelState.IsValid)
            {
                var column = _columns.GetEntity(model.EntityId);
                if (column != null)
                {
                    column.Name = model.Value;
                    _columns.UpdateEntity(column);
                    _columns.Save();
                }
            }
            return RedirectToAction("Board", new {id = model.BoardId});
        }

        [HttpPost]
        [Authorize]
        public IActionResult ChangeRecordValue(ChangeEntityModel model)
        {
            if (ModelState.IsValid)
            {
                var record = _records.GetEntity(model.EntityId);
                if (record != null)
                {
                    record.Value = model.Value;
                    _records.UpdateEntity(record);
                    _records.Save();
                }
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

        private IActionResult RemoveEntity<TEntity>(IEntityRepository<TEntity> repository, int id, int boardId)
            where TEntity : IEntity
        {
            repository.DeleteEntity(id);
            repository.Save();
            return RedirectToAction("Board", new {id = boardId});
        }
    }
}
