using CarRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    public class EngineCategoryController : AdminController
    {
        private readonly ICarService carService;

        public EngineCategoryController(ICarService carService)
        {
                this.carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var model = await this.carService.GetAllEngineCategory();

            return View(model);
        }
    }
}
