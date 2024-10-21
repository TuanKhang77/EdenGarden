
using EdenGarden_API.Data;
using EdenGarden_API.Helper;
using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections;

namespace EdenGarden_API.Controllers
{
    [Authorize]
    public class RoomController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomService _roomService;

        public RoomController(IRoomService baseService, ILogger<RoomController> logger, DataContext context)
        {
            _roomService = baseService;
            _logger = logger;
            _dataContext = context;
        }




        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        //{
        //    var rooms = await _dataContext.Room.ToListAsync();
        //    return rooms;
        //}

        ////[HttpGet]
        ////public ActionResult<IEnumerable<Room>> GetRooms()
        ////{
        ////    var rooms = _dataContext.Room.ToList();
        ////    return rooms;
        ////}

        //[HttpGet]
        //public async Task<ActionResult<Room>> GetRoom(int id)
        //{
        //    var rooms = await _dataContext.Room.FindAsync(id);
        //    return rooms;
        //    //return await _dataContext.Room.FindAsync(id)
        //}



        [HttpPost]
        public IActionResult Save([FromBody] Room roomModel)
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
                    response = _roomService.Create(roomModel);
                    
                    _logger.LogInformation("Object created successfully.");
                }
                else
                {
                    //create
                    //roomModel.CreatedUserId = Convert.ToInt32(userId);
                    response = _roomService.Update(roomModel);
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
                var result = _roomService.Delete(byid.Id);
                _logger.LogInformation("Object deleted successfully.");
                return NoContent();
           
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting object");
                return StatusCode(500,ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetById([FromBody] GetByRequest byid)
        {
            try
            {
                // int userId = Convert.ToInt32(User.Claims.First());
                var result = _roomService.GetRoomById(byid.Id);

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
                var rooms = _roomService.GetAll().ToList();
                _logger.LogInformation("Retrieved all objects successfully");
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all objects");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
