using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsApiController(IStatisticsService statisticsService)
        {
            this.statisticsService = statisticsService;
        }

        /// <summary>
        /// Get statistics about number of houses and rented houses
        /// </summary>
        /// <returns>Total houses and total rented houses</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetStatistics()
        {
            var model = await this.statisticsService.Total();

            return Ok(model);
        }
    }
}
