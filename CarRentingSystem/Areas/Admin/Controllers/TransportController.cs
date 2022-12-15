using CarRentingSystem.Areas.Admin.Models;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Areas.Admin.Controllers
{
    public class TransportController : AdminController
    {
        private readonly ITransportService transportService;
        private readonly IAgentService agentService;

        public TransportController(ITransportService transportService, IAgentService agentService)
        {
            this.transportService = transportService;
            this.agentService = agentService;
        }

        public async Task<IActionResult> All()
        {
            var model = await this.transportService.GetAllTransports();

            return View(model);
        }

        public async Task<IActionResult> Mine()
        {
            var myTransports = new MyTransportsViewModel();

            var adminUserId = this.User.Id();

            myTransports.RentedTransports = await this.transportService.AllTransportsByUserId(adminUserId);

            var adminAgentId = await this.agentService.GetAgentId(adminUserId);

            myTransports.AddedTransports = await this.transportService.AllTransportsByAgentId(adminAgentId);

            return View(myTransports);
        }


    }
}
