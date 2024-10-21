using EdenGarden_API.Data;
using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Services;
using EdenGarden_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EdenGarden_API.Controllers
{
    public class RoomTypeController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<RoomTypeController> _logger;
        private readonly IRoomTypeService _roomTypeService;
        private IPhotoService _photoService;
        public RoomTypeController(DataContext dataContext, ILogger<RoomTypeController> logger, IRoomTypeService roomTypeService, IPhotoService photoService)
        {
            _dataContext = dataContext;
            _logger = logger;
            _roomTypeService = roomTypeService;
            _photoService = photoService;
        }
        [HttpPost]
        public IActionResult GetById([FromBody] GetByRequest byid)
        {
            try
            {
                // int userId = Convert.ToInt32(User.Claims.First());
                var result = _roomTypeService.GetRoomTypeById(byid.Id);

                _logger.LogInformation("Retrieved object successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving object");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try
            {
                string userId = User.Claims.First().Value;
                var services = _roomTypeService.GetAll().ToList();
                _logger.LogInformation("Retrieved all objects successfully");
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all objects");
                return StatusCode(500, ex.Message);
            }
        }
        //[HttpPost]
        //public IActionResult Create([FromForm]CreateRoomTypeRequest roomModel)
        //{
        //    try
        //    {
        //        //string userId = User.Claims.First().Value;
        //        //roomModel.CreatedUserId = Convert.ToInt32(userId);

        //        Response response;
        //        string imgUrl = null;
        //        if (roomModel.Img != null)
        //        {
        //            var img = _photoService.AddPhoto(roomModel.Img);
        //            imgUrl = img.SecureUrl.AbsoluteUri;
        //        }

        //        //create
        //        //roomModel.ModifiedUserId = Convert.ToInt32(userId);
        //        var roomTypeEntry = new RoomType
        //        {
        //            Description = roomModel.Description,
        //            TypeName = roomModel.TypeName,
        //            NumofPeople = roomModel.NumofPeople,
        //            Services = roomModel.Services,
        //            Price = roomModel.Price,
        //            Img = imgUrl
        //        };
        //        response = _roomTypeService.Create(roomTypeEntry);

        //        _logger.LogInformation("Object created successfully.");

        //        return Ok(response);

        //    }
        //    catch (Exception ex)
        //    {

        //        _logger.LogError(ex, "An error occurred while create/update object");
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        //[HttpPost]
        //public IActionResult Update([FromBody] CreateRoomTypeRequest roomModel)
        //{
        //    try
        //    {
        //        //string userId = User.Claims.First().Value;
        //        //roomModel.CreatedUserId = Convert.ToInt32(userId);

        //        Response response;

        //        string imgUrl = null;
        //        if (roomModel.Img != null)
        //        {
        //            var img = _photoService.AddPhoto(roomModel.Img);
        //            imgUrl = img.SecureUrl.AbsoluteUri;
        //        }

        //        //update
        //        //roomModel.CreatedUserId = Convert.ToInt32(userId);
        //        var roomTypeEntry = new RoomType
        //        {
        //            Id = roomModel.Id,
        //            Description = roomModel.Description,
        //            TypeName = roomModel.TypeName,
        //            NumofPeople = roomModel.NumofPeople,
        //            Services = roomModel.Services,
        //            Price = roomModel.Price,
        //            Img = imgUrl
        //        };

        //        response = _roomTypeService.Update(roomTypeEntry);
        //        _logger.LogInformation("Object updated successfully.");


        //        return Ok(response);

        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while create/update object");
        //        return StatusCode(500, ex.Message);
        //    }
        //}


        [HttpPost]
        public IActionResult Save([FromBody] RoomType roomModel)
        {
            try
            {
                //string userId = User.Claims.First().Value;
                //roomModel.CreatedUserId = Convert.ToInt32(userId);

                Response response;

                if (roomModel.Id == 0)
                {
                    //update
                    //roomModel.ModifiedUserId = Convert.ToInt32(userId);
                    response = _roomTypeService.Create(roomModel);

                    _logger.LogInformation("Object created successfully.");
                }
                else
                {
                    //create
                    //roomModel.CreatedUserId = Convert.ToInt32(userId);
                    response = _roomTypeService.Update(roomModel);
                    _logger.LogInformation("Object updated successfully.");
                }

                return Ok(response);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while create/update object");
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public IActionResult Delete([FromBody] GetByRequest byid)
        {
            try
            {
                string userId = User.Claims.First().Value;
                var result = _roomTypeService.Delete(byid.Id);
                _logger.LogInformation("Object deleted successfully.");
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting object");
                return StatusCode(500, ex.Message);
            }
        }


    }
}
