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

    public class ClientController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ClientController> _logger;
        private readonly IClientService _clientService;

        public ClientController(IClientService baseService, ILogger<ClientController> logger, DataContext context)
        {
            _clientService = baseService;
            _logger = logger;
            _dataContext = context;
        }

        [HttpPost]
        public IActionResult Save([FromBody] Client clientModel)
        {
            try
            {
                //string userId = User.Claims.First().Value;
                //roomModel.CreatedUserId = Convert.ToInt32(userId);

                Response response;

                
                    //update
                    //roomModel.CreatedUserId = Convert.ToInt32(userId);
                    response = _clientService.Update(clientModel);
                    _logger.LogInformation("Object updated successfully.");
                    
                
               
                    //create
                    //roomModel.ModifiedUserId = Convert.ToInt32(userId);
                    //response = _clientService.Create(clientModel);

                    //_logger.LogInformation("Object created successfully.");
                

                return Ok(response);

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred while create/update object");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult GetById([FromBody] GetByRequest byid)
        {
            try
            {
                // int userId = Convert.ToInt32(User.Claims.First());
                var result = _clientService.GetClientById(byid.Id);

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
                var rooms = _clientService.GetAll().ToList();
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
