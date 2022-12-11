﻿using CarRentingSystem.Areas.Admin.Constants;
using CarRentingSystem.Constants;
using CarRentingSystem.Core.Contracts;
using CarRentingSystem.Core.Models.Car;
using CarRentingSystem.Core.Models.Transport;
using CarRentingSystem.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRentingSystem.Controllers
{
    [Authorize]
    public class TransportController : Controller
    {
        private readonly ITransportService transportService;
        private readonly IAgentService agentService;

        public TransportController(ITransportService transportService, IAgentService agentService)
        {
            this.transportService = transportService;
            this.agentService = agentService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            if (this.User.IsInRole(AdminConstants.AdminRoleName))
            {
                return RedirectToAction("Mine", "Transport", new { area = "Admin" });
            }

            var allTransports = await this.transportService.GetAllTransports();

            return View(allTransports);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            IEnumerable<AllTransportsViewModel> myTransports;

            var userId = User.Id();

            if ((await this.agentService.ExistById(userId)))
            {
                int agentId = await this.agentService.GetAgentId(userId);

                myTransports = await this.transportService.AllTransportsByAgentId(agentId);
            }
            else
            {
                if (!(await this.transportService.HasRentedCarByUserId(userId)))
                {
                    TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.HasNoRentedCars;
                    return RedirectToAction(nameof(All));
                }

                myTransports = await this.transportService.AllTransportsByUserId(userId);
            }

            return View(myTransports);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (await agentService.ExistById(User.Id()) == false)
            {
                TempData[AgentControllerConstants.ErrorMessage] = CarControllerConstants.AgentDoesNotExistError;
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            var model = new AddTransportModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTransportModel model)
        {
            //дали си агент ?
            if (await this.agentService.ExistById(User.Id()) == false)
            {
                TempData[AgentControllerConstants.SuccessMessage] = CarControllerConstants.AgentDoesNotExistError;
                return RedirectToAction(nameof(AgentController.Become), "Agent");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Some error occurred!");
                return View(model);
            }
            int agentId = await agentService.GetAgentId(User.Id());

            int id = await transportService.Create(model, agentId);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if ((await this.transportService.TransportExist(id) == false))
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportDoesNotExist;
                return RedirectToAction(nameof(All));
            }

            var model = await this.transportService.TransportDetailsById(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Rent(int id)
        {
            if ((await this.transportService.TransportExist(id)) == false)
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportDoesNotExist;
                return RedirectToAction(nameof(All));
            }

            if ((await this.transportService.HasAgentWithId(id, User.Id())))
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.HasNoPermision;
                return RedirectToAction(nameof(All));
            }

            if ((await this.transportService.IsRented(id)))
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportIsAlreadyRented;
                return RedirectToAction(nameof(All));
            }

            await this.transportService.RentTransport(id, User.Id());

            TempData[AgentControllerConstants.SuccessMessage] = TransportControllerConstants.TransportIsRentSuccessully;
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            if ((await this.transportService.TransportExist(id)) == false)
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportDoesNotExist;
                return RedirectToAction(nameof(All));
            }

            if ((await this.transportService.IsRented(id)) == false)
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportIsAlreadyRented;
                return RedirectToAction(nameof(All));
            }

            if ((await this.transportService.IsRentedByUserWithId(id, User.Id())) == false)
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportIsAlreadyRented;
                return RedirectToAction(nameof(All));
            }

            await this.transportService.LeaveTransport(id);

            TempData[TransportControllerConstants.SuccessMessage] = TransportControllerConstants.TransportIsLeavedSuccessfully;
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await transportService.TransportExist(id)) == false)
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportDoesNotExist;
                return RedirectToAction(nameof(All));
            }

            if ((await transportService.HasAgentWithId(id, User.Id())) == false && !User.IsAdmin())
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.HasNoPermision;
                return RedirectToAction(nameof(All));
            }

            var model = await this.transportService.TransportDetailsById(id);

            var transport = this.transportService.AddTransportModel(model);

            return View(transport);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddTransportModel model)
        {
            if ((await transportService.TransportExist(model.Id)) == false)
            {
                ModelState.AddModelError("", "Transport doesn`t exist!");
                return View(model);
            }

            if ((await transportService.HasAgentWithId(model.Id, User.Id())) == false && !User.IsAdmin())
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.HasNoPermision;
                return RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Some error occurred!");
                return View(model);
            }

            await this.transportService.TransportEdit(model.Id, model);

            TempData[TransportControllerConstants.SuccessMessage] = TransportControllerConstants.TransportEditedSuccessfully;
            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await transportService.TransportExist(id)) == false)
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportDoesNotExist;
                return RedirectToAction(nameof(All));
            }

            if ((await this.transportService.HasAgentWithId(id, User.Id())) == false && !User.IsAdmin())
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.HasNoPermision;
                return RedirectToAction(nameof(All));
            }

            var transport = await transportService.TransportDetailsById(id);

            return View(transport);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, AllTransportsViewModel model)
        {
            if ((await transportService.TransportExist(id)) == false)
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.TransportDoesNotExist;
                return RedirectToAction(nameof(All));
            }

            if ((await transportService.HasAgentWithId(id, User.Id())) == false && !User.IsAdmin())
            {
                TempData[TransportControllerConstants.ErrorMessage] = TransportControllerConstants.HasNoPermision;
                return RedirectToAction(nameof(All));
            }

            await this.transportService.DeleteTransport(id);

            TempData[TransportControllerConstants.SuccessMessage] = TransportControllerConstants.TransportDeletedSuccessfully;
            return RedirectToAction(nameof(All));
        }
    }
}
