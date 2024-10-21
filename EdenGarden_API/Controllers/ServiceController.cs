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

    public class ServiceController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ServiceController> _logger;
        private readonly IServiceService _serviceService;
        public ServiceController(DataContext dataContext, ILogger<ServiceController> logger, IServiceService serviceService)
        {
            _dataContext = dataContext;
            _logger = logger;
            _serviceService = serviceService;
        }

        [HttpPost]
        public IActionResult Save([FromBody] Service serviceModel)
        {
            try
            {
                //string userId = User.Claims.First().Value;
                //roomModel.CreatedUserId = Convert.ToInt32(userId);

                Response response;

                if (serviceModel.Id == 0)
                {
                    //update
                    //roomModel.ModifiedUserId = Convert.ToInt32(userId);
                    response = _serviceService.Create(serviceModel);

                    _logger.LogInformation("Object created successfully.");
                }
                else
                {
                    //create
                    //roomModel.CreatedUserId = Convert.ToInt32(userId);
                    response = _serviceService.Update(serviceModel);
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
                var result = _serviceService.Delete(byid.Id);
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
                var result = _serviceService.GetServiceById(byid.Id);

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
                // string userId = User.Claims.First().Value;
                var services = _serviceService.GetAll().ToList();
                _logger.LogInformation("Retrieved all objects successfully");
                return Ok(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all objects");
                return StatusCode(500, ex.Message);
            }
        }

    }
}
