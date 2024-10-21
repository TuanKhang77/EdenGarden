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

    public class BookedRoomController : BaseController
    {
        private readonly ILogger<BookedRoomController> _logger;
        private IBookedRoomService _bookedRoomService;
        public BookedRoomController(ILogger<BookedRoomController> logger,
            DataContext context,
            IServiceService serviceService,
            IBookedRoomService bookedRoomService)
        {
            _logger = logger;
            _bookedRoomService = bookedRoomService;
        }
        [HttpPost]
        public IActionResult Save([FromBody] BookedRoom roomModel)
        {
            try
            {
                //string userId = User.Claims.First().Value;
                Response response;
                response = _bookedRoomService.Update(roomModel);
                _logger.LogInformation("Object updated successfully.");
                

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
                var result = _bookedRoomService.Delete(byid.Id);
                _logger.LogInformation("Object deleted successfully.");
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting object");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetById([FromBody] GetByRequest byid)
        {
            try
            {
                // int userId = Convert.ToInt32(User.Claims.First());
                var result = _bookedRoomService.GetBookedRoomById(byid.Id);

                _logger.LogInformation("Retrieved object successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving object");
                return StatusCode(500, ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult GetAll()
        {
            try
            {
                string userId = User.Claims.First().Value;
                var rooms = _bookedRoomService.GetAll().ToList();
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
