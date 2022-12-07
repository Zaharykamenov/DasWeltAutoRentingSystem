using CarRentingSystem.Areas.Admin.Models;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    public class CarController : AdminController
    {
        private readonly ICarService carService;
        private readonly IAgentService agentService;

        public CarController(ICarService carService, IAgentService agentService)
        {
            this.carService = carService;
            this.agentService = agentService;
        }

        public async Task<IActionResult> Mine()
        {
            var myCars = new MyCarsViewModel();

            var adminUserId = this.User.Id();

            myCars.RentedCars = await this.carService.AllCarsByUserId(adminUserId);

            var adminAgentId = await this.agentService.GetAgentId(adminUserId);

            myCars.AddedCars = await this.carService.AllCarsByAgentId(adminAgentId);

            return View(myCars);
        }

        public async Task<IActionResult> All()
        {
            var model = await this.carService.GetAllExistedCar();

            return View(model);
        }
    }
}
