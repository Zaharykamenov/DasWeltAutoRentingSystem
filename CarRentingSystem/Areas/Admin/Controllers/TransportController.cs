using CarRentingSystem.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    public class TransportController : AdminController
    {
        private readonly ITransportService transportService;

        public TransportController(ITransportService transportService)
        {
            this.transportService = transportService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.transportService.GetAllTransports();

            return View(model);
        }


    }
}
