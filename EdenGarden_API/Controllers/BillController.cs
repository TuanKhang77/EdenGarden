using EdenGarden_API.Data;
using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Models.Response;
using EdenGarden_API.Services;
using EdenGarden_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EdenGarden_API.Controllers
{
    public class BillController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<BillController> _logger;
        private readonly IBillService _billService;
       
        public BillController(IBillService baseService, ILogger<BillController> logger, DataContext context )
        {
            _billService = baseService;
            _logger = logger;
            _dataContext = context;
            
        }

        [HttpPost]
        public IActionResult Save([FromBody] Bill billModel)
        {
            try
            {
                Response response;
                //update
                //roomModel.CreatedUserId = Convert.ToInt32(userId);
                response = _billService.Update(billModel);
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
        public IActionResult GetById([FromBody] GetByRequest byid)
        {
            try
            {
                // int userId = Convert.ToInt32(User.Claims.First());
                var result = _billService.GetBillById(byid.Id);

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
                var rooms = _billService.GetAll().ToList();
                _logger.LogInformation("Retrieved all objects successfully");
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all objects");
                return StatusCode(500, ex.Message);
            }
        }


        //[AllowAnonymous]
        //[HttpPost]
        //public IActionResult Payment()
        //{
        //    var model = new VnPayRequestModel
        //    {
        //        Amount = 1234,
        //        CreatedDate = new DateTime(2024, 11, 24, 16, 0, 0),
        //        OrderId = (int)DateTime.Now.Ticks
        //    };
        //    _vnPayService.CreatePaymentUrlAsync(HttpContext, model);
        //    return NoContent();
        //}


        //[AllowAnonymous]
        //[HttpPost] 
        //public IActionResult VnPayReturn ()
        //{
        //    //var response = _vnPayService.PaymentExercute(Request.Query);

        //    return NoContent();
        //}
    }
}
