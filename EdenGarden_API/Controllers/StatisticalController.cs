using EdenGarden_API.Data;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Services;
using EdenGarden_API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EdenGarden_API.Controllers
{
    public class StatisticalController : BaseController
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<StatisticalController> _logger;
        private readonly IStatisticalService _statisticalService;

        public StatisticalController(IStatisticalService baseService, ILogger<StatisticalController> logger, DataContext context)
        {
            _statisticalService = baseService;
            _logger = logger;
            _dataContext = context;
        }

        [HttpPost]
        public IActionResult GetRoomRecordsByYear([FromBody] GetByRequest byYear)
        {
            try
            {
                // int userId = Convert.ToInt32(User.Claims.First());
                var result = _statisticalService.GetRoomRecordsByYear(byYear.year);

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
        public IActionResult GetAmountRecordsByMonth([FromBody] GetByRequest byYear)
        {
            try
            {
                // int userId = Convert.ToInt32(User.Claims.First());
                var result = _statisticalService.GetAmountRecordsByMonth(byYear.year);

                _logger.LogInformation("Retrieved object successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving object");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
